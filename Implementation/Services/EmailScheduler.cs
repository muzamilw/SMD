using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class EmailScheduler
    {
        [Dependency]
        private static IEmailManagerService EmailManagerService { get; set; }
        public static void UserTrainingEmailAfterThreeDays(Registry registry)
        {

            // Registration of Debit Process Scheduler Run after every 7 days 
         //   registry.Schedule(UserTrainingEmail).ToRunEvery(1).Days();
           // registry.Schedule(UserTrainingEmail).ToRunNow().AndEvery(1).Minutes();
        }

        public static void MonitorQueue(Registry registry)
        {

            // Registration of Debit Process Scheduler Run after every 7 days 
           // registry.Schedule(SendEmailFromQueue).ToRunNow().AndEvery(1).Minutes();
        }
        public static void UserTrainingEmail()
        {
            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://smdpreview.com/reportViewers/user.aspx?userID=6CFEBEED-B8A0-403F-891E-FB53AB64DE3C&StartDate=04/06/2016&EndDate=04/30/2016&mode=email");
            //myRequest.Method = "GET";
            //WebResponse myResponse = myRequest.GetResponse();
            //StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            //string result = sr.ReadToEnd();
            //sr.Close();
            //myResponse.Close();
        }
        public static void SendEmailFromQueue()
        {
            try
            {
                using (var db = new BaseDbContext())
                {
                    bool res = false;

                    List<EmailQueue> allrecords = (from c in db.EmailQueues
                                                   where c.IsDeliverd == 0 && c.AttemptCount < 5
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
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}
