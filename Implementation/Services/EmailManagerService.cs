using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using SMD.ExceptionHandling;
using SMD.Implementation.Identity;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Email Manager Service
    /// </summary>
    public class EmailManagerService : IEmailManagerService
    {
        #region private

        public string SubscribedServices { get; set; }


        public string CustomerRegion { get; set; }


        public string PaymentAmount { get; set; }


        public string Assignedto { get; set; }

        public string TicketRefCode { get; set; }

        private string developerResponse;
        public string DeveloperResponse
        {
            set { developerResponse = value; }
            get { return developerResponse; }
        }

        private string developerDueDate;
        public string DeveloperDueDate
        {
            set { developerDueDate = value; }
            get { return developerDueDate; }
        }
        
        public string Assignedby { get; set; }
        
        public string InstanceLink { get; set; }

        private string ticketId;
        public string TicketId
        {
            set { ticketId = value; }
            get { return ticketId; }
        }

        public string CompanyName { get; set; }

        public string UserPassword { get; set; }

        public string CountryName { get; set; }

        public string PhoneNo { get; set; }

        public string CMessage { get; set; }
        public string NewsLetterSubCode { get; set; }

        public int Mid { get; set; }

        public string Muser { get; set; }

        private string sfname;
        public string Fname { get; set; }

        private const string Slname = "";
        public string Lname { get; set; }

        private string sfemail;
        public string FEmail { get; set; }

        private string smailsubject;
        public string Subj { get; set; }

        public string MBody { get; set; }

        public List<string> MMailto
        {
            set;
            get;
        }

        public string Services { get; set; }

        public string ServicePrice { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string StrettAddress { get; set; }

        public string BillingMonth { get; set; }

        public string DueDate { get; set; }

        public string CustomerAccountNo { get; set; }

        public string TotalAmount { get; set; }

        public string InVoiceCode { get; set; }
        
        public string ReceiptBody { get; set; }
        
        public string AccountManager { get; set; }

        public string AccountManagerNo { get; set; }

        public string TemplateName { get; set; }

        public string SharerName { get; set; }

        public string EmailConfirmationLink { get; set; }
        public string SharedDesignLink { get; set; }
        public string CreateAccountLink { get; set; }
        public string PasswordResetLink { get; set; }
        public string VoucherDescription { get; set; }
        public string VoucherValue { get; set; }
        /// <summary>
        /// Sends Email
        /// </summary>
        private async Task SendEmail()
        {
// ReSharper disable SuggestUseVarKeywordEvident
            MailMessage oMailBody = new MailMessage();
// ReSharper restore SuggestUseVarKeywordEvident

            int id = Mid;

            if (id == 0)
            {
                throw new Exception("MailId must be assigned");
            }

            if (MMailto.Count < 1)
            {
                throw new Exception("Provide an email Address");
            }

            SystemMail email = systemMailRepository.Find(id);

            if (email != null)
            {
                sfname = email.FromName;
                sfemail = email.FromEmail;
                smailsubject = email.Subject;
                MBody = email.Body;
            }

            smailsubject = smailsubject.Replace("++username++", Muser);
            smailsubject = smailsubject.Replace("++firstname++", Fname);
            smailsubject = smailsubject.Replace("++lastname++", Lname);
            smailsubject = smailsubject.Replace("++MailSubject++", Subj);
            MBody = MBody.Replace("++username++", Muser);
            MBody = MBody.Replace("++firstname++", Fname);
            MBody = MBody.Replace("++lastname++", Lname);
            MBody = MBody.Replace("++CurrentDateTime++", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " GMT");
            MBody = MBody.Replace("++EmailConfirmationLink++", EmailConfirmationLink);
            if (Mid == (int)EmailTypes.ResetPassword)
            {
                MBody = MBody.Replace("++PasswordResetLink++", PasswordResetLink);
            }
            if (Mid == (int)EmailTypes.Voucher)
            {
                MBody = MBody.Replace("++VoucherDescription++", VoucherDescription);
                MBody = MBody.Replace("++VoucherValue++", VoucherValue);
            }
            MBody = MBody.Replace("++Fname++", Fname);
            oMailBody.IsBodyHtml = true;
            oMailBody.From = new MailAddress(sfemail, sfname);
            oMailBody.Subject = smailsubject;
            oMailBody.Body = MBody;

            if (id != 17)
            {
                oMailBody.To.Clear();
                foreach (var elememnt in MMailto)
                {
                    oMailBody.To.Add(new MailAddress(elememnt, sfname + " " + Slname));
                }
            }
            else
            {
                oMailBody.To.Clear();
                foreach (var elememnt in MMailto)
                {
                    oMailBody.To.Add(elememnt);
                }
            }

            oMailBody.Priority = MailPriority.Normal;
            string smtp = ConfigurationManager.AppSettings["SMTPServer"];
            string mailAddress = ConfigurationManager.AppSettings["SMTPUser"];
            string mailPassword = ConfigurationManager.AppSettings["SMTPPassword"];
// ReSharper disable SuggestUseVarKeywordEvident
            SmtpClient objSmtpClient = new SmtpClient(smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(mailAddress, mailPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.Credentials = mailAuthentication;
            MMailto.Clear();
            try
            {
                await objSmtpClient.SendMailAsync(oMailBody);
            }
            catch (Exception)
            {
                throw new SMDException(LanguageResources.EmailManagerService_FailedToSendEmail);
            }
        }

        /// <summary>
        /// Send Email Non-Async
        /// </summary>
// ReSharper disable UnusedMember.Local
        private bool SendEmailNoAsync()
// ReSharper restore UnusedMember.Local
        {
            MailMessage oMailBody = new MailMessage();

            int id = Mid;

            if (id == 0)
            {
                throw new Exception("MailId must be assigned");
            }

            if (MMailto.Count < 1)
            {
                throw new Exception("Provide an email Address");
            }

            SystemMail email = systemMailRepository.Find(id);

            if (email != null)
            {
                sfname = email.FromName;
                sfemail = email.FromEmail;
                smailsubject = email.Subject;
                MBody = email.Body;
            }

            smailsubject = smailsubject.Replace("++TicketRefCode++", TicketRefCode);
            smailsubject = smailsubject.Replace("++username++", Muser);
            smailsubject = smailsubject.Replace("++firstname++", Fname);
            smailsubject = smailsubject.Replace("++lastname++", Lname);
            smailsubject = smailsubject.Replace("++MailSubject++", Subj);
            MBody = MBody.Replace("++ContactUsMessage++", CMessage);
            MBody = MBody.Replace("++username++", Muser);
            MBody = MBody.Replace("++instanceLink++", InstanceLink);
            MBody = MBody.Replace("++ticketid++", ticketId);
            MBody = MBody.Replace("++userpassword++", UserPassword);
            MBody = MBody.Replace("++TicketRefCode++", TicketRefCode);
            MBody = MBody.Replace("++firstname++", Fname);
            MBody = MBody.Replace("++lastname++", Lname);
            MBody = MBody.Replace("++AssignedTo++", Assignedto);
            MBody = MBody.Replace("[Assigned By]", Assignedby);
            MBody = MBody.Replace("++CompanyName++", CompanyName);
            MBody = MBody.Replace("++CurrentDateTime++", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " GMT");
            MBody = MBody.Replace("++DeveloperResponse++", developerResponse);
            MBody = MBody.Replace("++DeveloperDueDate++", developerDueDate);
            MBody = MBody.Replace("++Country++", CountryName);
            MBody = MBody.Replace("++Phone++", PhoneNo);
            MBody = MBody.Replace("++EmailConfirmationLink++", EmailConfirmationLink);
            MBody = MBody.Replace("++PasswordResetLink++", PasswordResetLink);
            MBody = MBody.Replace("++TemplateName++", TemplateName);
            MBody = MBody.Replace("++Fname++", Fname);
            oMailBody.IsBodyHtml = true;
            oMailBody.From = new MailAddress(sfemail, sfname);
            oMailBody.Subject = smailsubject;
            oMailBody.Body = MBody;

            if (id != 17)
            {
                oMailBody.To.Clear();
                foreach (var elememnt in MMailto)
                {
                    oMailBody.To.Add(new MailAddress(elememnt, sfname + " " + Slname));
                }
            }
            else
            {
                oMailBody.To.Clear();
                foreach (var elememnt in MMailto)
                {
                    oMailBody.To.Add(elememnt);
                }
            }

            oMailBody.Priority = MailPriority.Normal;
            string smtp = ConfigurationManager.AppSettings["SMTPServer"];
            string mailAddress = ConfigurationManager.AppSettings["SMTPUser"];
            string mailPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            SmtpClient objSmtpClient = new SmtpClient(smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(mailAddress, mailPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.Credentials = mailAuthentication;
            MMailto.Clear();
            objSmtpClient.Send(oMailBody);
            return true;
        }

        private readonly ISystemMailsRepository systemMailRepository;
        
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EmailManagerService(ISystemMailsRepository systemMailRepository)
        {
            if (systemMailRepository == null)
            {
                throw new ArgumentNullException("systemMailRepository");
            }
            this.systemMailRepository = systemMailRepository;
            MMailto = new List<string>();
        }

        #endregion

        #region Public

        /// <summary>
        /// User Manager
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        /// <summary>
        /// Send Invoice
        /// </summary>
       

        /// <summary>
        /// Send Error Log
        /// </summary>
        public bool SendErrorLog(string errormsg)
        {
            MailMessage oMailBody = new MailMessage
                                    {
                                        IsBodyHtml = true,
                                        From = new MailAddress("info@myprintcloud.com", "Sell My Data"),
                                        Subject = "WebHook Handler Exception",
                                        Body =
                                            "<p>WebHook Handler have been failed due to following error.<br /><br />" +
                                            errormsg + "</p>",
                                        Priority = MailPriority.Normal
                                    };


            string smtp = ConfigurationManager.AppSettings["SMTPServer"];
            string mailAddress = ConfigurationManager.AppSettings["smtpUser"];
            string mailPassword = ConfigurationManager.AppSettings["smtpPassword"];
            string mailAddressTo = ConfigurationManager.AppSettings["contactInternalEmail"];
            oMailBody.To.Add(mailAddressTo);
            SmtpClient objSmtpClient = new SmtpClient(smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(mailAddress, mailPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.Credentials = mailAuthentication;
            objSmtpClient.Send(oMailBody);
            return true;
        }

        /// <summary>
        /// Account Verification Email
        /// </summary>
        public async Task SendAccountVerificationEmail(User oUser, string emailConfirmationLink)
        {
            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int) EmailTypes.VerifyAccount;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                EmailConfirmationLink = emailConfirmationLink;
                await SendEmail();
            }
            else
            {
                throw new Exception("Customer is null");    
            }
        }
        public bool SendInovice()
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            MailMessage oMailBody = new MailMessage();
            // ReSharper restore SuggestUseVarKeywordEvident
            int id = Mid;

            if (id == 0)
            {
                throw new Exception("Mail Id must be assigned");
            }

            if (MMailto.Count < 1)
            {
                throw new Exception("Provide an email Address");
            }

            SystemMail email = systemMailRepository.Find(id);

            if (email != null)
            {
                sfname = email.FromName;
                sfemail = email.FromEmail;
                smailsubject = email.Subject;
                MBody = email.Body;
            }

            smailsubject = smailsubject.Replace("++TicketRefCode++", TicketRefCode);
            smailsubject = smailsubject.Replace("++username++", Muser);
            smailsubject = smailsubject.Replace("++firstname++", Fname);
            smailsubject = smailsubject.Replace("++lastname++", Lname);
            smailsubject = smailsubject.Replace("++MailSubject++", Subj);
            MBody = MBody.Replace("++AccNo++", CustomerAccountNo);
            MBody = MBody.Replace("++City++", City);
            MBody = MBody.Replace("++StreetAddress++", StrettAddress);
            MBody = MBody.Replace("++downloadLink++", InstanceLink);
            MBody = MBody.Replace("++PostalCode++", PostalCode);
            MBody = MBody.Replace("++PhoneNo++", PhoneNo);
            MBody = MBody.Replace("++Services++", Services);
            MBody = MBody.Replace("++Price++", ServicePrice);
            MBody = MBody.Replace("++BillingMonth++", BillingMonth);
            MBody = MBody.Replace("++TotalAmount++", TotalAmount);
            MBody = MBody.Replace("++CompanyName++", CompanyName);
            MBody = MBody.Replace("++DueDate++", DueDate);
            MBody = MBody.Replace("++InVoiceCode++", InVoiceCode);

            oMailBody.IsBodyHtml = true;
            oMailBody.From = new MailAddress(sfemail, sfname);
            oMailBody.Subject = smailsubject;
            oMailBody.Body = MBody;

            if (id != 17)
            {
                oMailBody.To.Clear();
                foreach (var elememnt in MMailto)
                {
                    oMailBody.To.Add(new MailAddress(elememnt, sfname + " " + Slname));
                }
            }
            else
            {
                oMailBody.To.Clear();
                foreach (var elememnt in MMailto)
                {
                    oMailBody.To.Add(elememnt);
                }
            }

            oMailBody.Priority = MailPriority.Normal;
            string smtp = ConfigurationManager.AppSettings["SmtpServer"];
            string mailAddress = ConfigurationManager.AppSettings["MailFrom"];
            string mailPassword = ConfigurationManager.AppSettings["MailPassword"];
            SmtpClient objSmtpClient = new SmtpClient(smtp);
            NetworkCredential mailAuthentication = new NetworkCredential(mailAddress, mailPassword);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.Credentials = mailAuthentication;
            objSmtpClient.Send(oMailBody);
            MMailto.Clear();
            return true;

        }
        /// <summary>
        /// Send Email on Question Approval 
        /// </summary>
        public async Task SendQuestionApprovalEmail(string aspnetUserId)
        {
            var oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.QuestionApproved;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                await SendEmail();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }
        /// <summary>
        ///Send Email on Question Rejection 
        /// </summary>
        public async Task SendQuestionRejectionEmail(string aspnetUserId)
        {
            var oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.QuestionRejected;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                await SendEmail();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }
        /// <summary>
        /// Registration Success Email
        /// </summary>
        public async Task SendRegisrationSuccessEmail(string aspnetUserId)
        {
            User oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.RegistrationConfirmed;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                await SendEmail();
            }
            else
            {
                throw new Exception("Customer is null");
            }
        }
        
        /// <summary>
        /// Send Password Reset Email
        /// </summary>
        public async Task SendPasswordResetLinkEmail(User oUser, string passwordResetLink)
        {
            MMailto.Add(oUser.Email);
            Mid = (int)EmailTypes.ResetPassword;
            Muser = oUser.Email;
            Fname = oUser.FullName;
            PhoneNo = oUser.PhoneNumber;
            PasswordResetLink = passwordResetLink;
            await SendEmail();
        }
        // ReSharper restore SuggestUseVarKeywordEvident

        /// <summary>
        ///Send Email when Collection scheduler run
        /// </summary>
        public async Task SendCollectionRoutineEmail(string aspnetUserId)
        {
            User oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.CollectionMade;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                await SendEmail();
            }
            else
            {
                throw new Exception("Customer is null");
            } 
        }

        /// <summary>
        /// Send Email when Collection scheduler run
        /// </summary>
        public async Task SendVoucherEmail(string aspnetUserId, string voucherDescription, string voucherValue, string voucherImage)
        {
            User oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.Voucher;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                VoucherValue = voucherValue;
                VoucherDescription = voucherDescription;
                await SendEmail();
            }
            else
            {
                throw new Exception("Customer is null");
            }
        }


        /// <summary>
        ///Send Email when Payout scheduler run
        /// </summary>
        public async Task SendPayOutRoutineEmail(string aspnetUserId)
        {
            User oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.PayoutMade;
                Muser = oUser.Email;
                Fname = oUser.FullName;
                PhoneNo = oUser.PhoneNumber;
                await SendEmail();
            }
            else
            {
                throw new Exception("Customer is null");
            }  
        }
        #endregion

    }
}
