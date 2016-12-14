using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class EmailScheduler
    {
        [Dependency]
        private static IEmailManagerService EmailManagerService { get; set; }
        private static string SiteUrl { get; set; }
        private static System.Web.HttpContext RequestContext { get; set; }
        public static void UserAccountDetailScheduler(Registry registry, System.Web.HttpContext context)
        {
            RequestContext = context;
            if (context.Handler != null)
            {
                SiteUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Host;
            }
            else 
            {
                SiteUrl = "http://manage.cash4ads.com";
            }
           
            // Registration of Debit Process Scheduler Run after every 7 days 
         //   registry.Schedule(UserTrainingEmail).ToRunEvery(1).Days();
          //  registry.Schedule(MonthlyAccountDetailsOfUser).AndEvery(1).Months();
        }

        public static void MonitorQueue(Registry registry)
        {

            // Registration of Debit Process Scheduler Run after every 7 days 
            //registry.Schedule(SendEmailFromQueue).ToRunNow().AndEvery(10).Minutes();
        }
        public static void MonthlyAccountDetailsOfUser()
        {
            List<User> allUsers = null;
            DateTime todayDate = DateTime.Today;
            DateTime LastMoth = new DateTime(todayDate.Year, todayDate.Month, 1);
            DateTime LastMonthStartDT = LastMoth.AddMonths(-1);
            DateTime LastMonthEndDT = LastMoth.AddDays(-1);
            string StartDate = LastMonthStartDT.Month < 10 ? "0" + LastMonthStartDT.Month : LastMonthStartDT.Month.ToString();
            StartDate = StartDate + "/" + "0" + LastMonthStartDT.Day + "/" + LastMonthStartDT.Year;
            string LastDate = LastMonthEndDT.Month < 10 ? "0" + LastMonthEndDT.Month : LastMonthEndDT.Month.ToString();
            LastDate = LastDate + "/" + LastMonthEndDT.Day + "/" + LastMonthEndDT.Year;
            string FileName = "";
            Random randgn = new Random();
            using (var db = new BaseDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                allUsers = db.Users.ToList();

                if (allUsers != null)
                {
                    foreach (User user in allUsers)
                    {
                        FileName = user.FullName + randgn.Next(4) + ".pdf";
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(SiteUrl);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            string url = "reportViewers/user.aspx?userID=" + user.Id + "&StartDate=" + StartDate + "&EndDate=" + LastDate + "&mode=email&FileName=" + FileName;
                            var response = client.GetAsync(url);
                            if (response.Result.IsSuccessStatusCode)
                            {
                                EmailQueue eqobj = new EmailQueue();
                                eqobj.Body = "<p> Hi " + user.FullName + ",  </p><p> Please review your attached Cash4Ads account e-Statement. </p>";
                                eqobj.Subject = "";
                                eqobj.To = user.Email;
                                eqobj.ToName = user.FullName;
                                eqobj.FromName = "Cash4Ads Team";
                                eqobj.EmailFrom = "info@myprintcloud.com";
                                eqobj.SMTPUserName = ConfigurationManager.AppSettings["SMTPUser"];
                                eqobj.SMTPServer = ConfigurationManager.AppSettings["SMTPServer"];
                                eqobj.SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"];
                                eqobj.SendDateTime = DateTime.Now;
                                eqobj.FileAttachment = "/SMD_Content/EmailAttachments/" + FileName + "|";
                                db.EmailQueues.Add(eqobj);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
        public static void SendEmailFromQueue()
        {
            try
            {
                using (var db = new BaseDbContext())
                {
                    bool res = false;

                    List<EmailQueue> allrecords = (from c in db.EmailQueues
                                                   where c.IsDeliverd != 1 && c.AttemptCount != 5
                                                   select c).ToList();

                    if (allrecords != null)
                    {
                        string ErrorMsg = string.Empty;

                        foreach (EmailQueue record in allrecords)
                        {
                            ErrorMsg = string.Empty;

                            if (string.IsNullOrEmpty(record.SMTPPassword) || string.IsNullOrEmpty(record.SMTPUserName) || string.IsNullOrEmpty(record.SMTPServer))
                            {
                                record.ErrorResponse = "smtp Settings not found.";
                                record.AttemptCount++;
                                db.SaveChanges();
                            }
                            else
                            {
                                if (SendEmail(record, out ErrorMsg))
                                {
                                    if (record.FileAttachment != null)
                                    {
                                        string filePath = string.Empty;
                                        string[] Allfiles = record.FileAttachment.Split('|');
                                        foreach (var file in Allfiles)
                                        {
                                            filePath = RequestContext.Server.MapPath(file);
                                            if (File.Exists(filePath))
                                                File.Delete(filePath);
                                        }
                                    }

                                    db.EmailQueues.Remove(record);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    record.ErrorResponse = ErrorMsg;
                                    record.AttemptCount++;
                                    db.SaveChanges();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static bool SendEmail(EmailQueue oEmailBody, out string ErrorMsg)
        {
            try
            {
                bool isFileExists = true;

                if (string.IsNullOrEmpty(oEmailBody.EmailFrom))
                {
                    ErrorMsg = "";
                    return false;
                }
                MailMessage objMail = new MailMessage();
                bool retVal = false;

                string smtp = oEmailBody.SMTPServer;
                string SmtpUserName = oEmailBody.SMTPUserName;
                string SenderPassword = oEmailBody.SMTPPassword;
                string FromEmail = oEmailBody.EmailFrom;
                string FromName = oEmailBody.FromName;
                string ToName = oEmailBody.ToName;
                string MailTo = oEmailBody.To;
                string CC = oEmailBody.Cc;


                Attachment data = null;
                if (oEmailBody.FileAttachment != null)
                {
                    string[] Allfiles = oEmailBody.FileAttachment.Split('|');
                    foreach (string temp in Allfiles)
                    {
                        if (temp != "")
                        {
                            string fname = temp;
                            if (temp.Contains('_'))
                            {
                                string[] abc = temp.Split('_');
                                fname = abc[abc.Length - 1];
                            }
                            else
                            {
                                string[] abc = temp.Split('/');
                                fname = abc[abc.Length - 1];
                            }

                            string FilePath = RequestContext.Server.MapPath(temp);
                            if (File.Exists(FilePath))
                            {
                                data = new Attachment(FilePath, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = data.ContentDisposition;
                                disposition.CreationDate = System.IO.File.GetCreationTime(FilePath);
                                disposition.ModificationDate = System.IO.File.GetLastWriteTime(FilePath);
                                disposition.ReadDate = System.IO.File.GetLastAccessTime(FilePath);
                                disposition.FileName = fname;
                                objMail.Attachments.Add(data);
                            }
                            else
                            {
                                isFileExists = false;
                            }
                        }
                    }
                }

                if (isFileExists == true)
                {
                    SmtpClient objSmtpClient = new SmtpClient(smtp);
                    objSmtpClient.Credentials = new NetworkCredential(SmtpUserName, SenderPassword);
                    objMail.From = new MailAddress(FromEmail, FromName);
                    objMail.To.Add(new MailAddress(MailTo, ToName));
                    if (!string.IsNullOrEmpty(CC))
                    {
                        if (!string.IsNullOrWhiteSpace(CC))
                            objMail.CC.Add(new MailAddress(CC));
                    }

                    objMail.IsBodyHtml = true;
                    objMail.Body = oEmailBody.Body;
                    objMail.Subject = oEmailBody.Subject;

                    objSmtpClient.Send(objMail);

                    if (data != null)
                    {
                        objMail.Attachments.Remove(data);
                        data.Dispose();
                    }
                    retVal = true;
                    ErrorMsg = "";


                    objMail.Dispose();
                    if (objMail != null)
                        objMail = null;

                    return retVal;

                }
                else
                {
                    ErrorMsg = "Attachment not found.";
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }






        public static void DealsRelatedNotifications(Registry registry, System.Web.HttpContext context)
        {
            RequestContext = context;
            if (context.Handler != null)
            {
                SiteUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Host;
            }
            else
            {
                SiteUrl = "http://manage.cash4ads.com";
            }

          

            // new dealls related emails in different modes of discount types
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(1)).ToRunNow().AndEvery(1).Days().At(10, 0);
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(2)).ToRunNow().AndEvery(1).Days().At(10, 10);
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(3)).ToRunNow().AndEvery(1).Days().At(10, 20);
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(4)).ToRunNow().AndEvery(1).Days().At(10, 30);
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(5)).ToRunNow().AndEvery(1).Days().At(10, 40);
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(6)).ToRunNow().AndEvery(1).Days().At(10, 50);
            registry.Schedule(() => EmailManagerService.SendNewDealsEmail(7)).ToRunNow().AndEvery(1).Days().At(11, 0);
            //  registry.Schedule(MonthlyAccountDetailsOfUser).AndEvery(1).Months();



            //performance emails
            registry.Schedule(() => EmailManagerService.SendCampaignPerformanceEmails()).ToRunEvery(0).Weeks().On(DayOfWeek.Monday).At(9, 0);


            registry.Schedule(() => EmailManagerService.SendCampaignPerformanceEmails()).ToRunEvery(0).Weeks().On(DayOfWeek.Monday).At(9, 0);



            
        }
    }
}
