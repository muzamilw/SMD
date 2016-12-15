﻿using System;
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
using System.Linq;

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

        public string feedback { get; set; }

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

        public string DeleteAccountLink { get; set; }


        public string VoucherDescription { get; set; }
        public string VoucherValue { get; set; }

        public string CompanyNameInviteUser { get; set; }

        public string FullNameInviteUser { get; set; }


        public string InviteURL { get; set; }
        public string AdvertiserLogoURL { get; set; }

        public string BuyItLogoURL { get; set; }

        public string BuyItLine1 { get; set; }

        public string BuyItLine2 { get; set; }

        public string BuyItURL { get; set; }
        public string CampaignName { get; set; }


        public string CampaignClicksPerDay { get; set; }

        public string CampaignVideoPath { get; set; }

        public string CampaignVideoImage { get; set; }

        public string CampaignBannerImage { get; set; }

        public string DealNoOfDays { get; set; }

        public string UserDealsHTML { get; set; }

        public string ctlw { get; set; }
        public string ctpq { get; set; }
        public string trendc { get; set; }
        public string alw { get; set; }
        public string apw { get; set; }
        public string trenda { get; set; }
        public string clickthroughcolor { get; set; }
        public string answercolor { get; set; }

        public string winnerpoll { get; set; }
        public string winnerpollperc { get; set; }

        public string pollanswercount { get; set; }

        public string RejectionReason { get; set; }

        public string PaymentFailedReason { get; set; }

        public string PaymentFailedAttempt { get; set; }

        public string NextPaymentAttempt { get; set; }


        public string Reviewer { get; set; }

        public string Review { get; set; }

        public string ReviewRating { get; set; }


        public string SignupLocation { get; set; }

        public string RoleName { get; set; }
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
            smailsubject = smailsubject.Replace("++companyname++", CompanyNameInviteUser);
            smailsubject = smailsubject.Replace("++inviter++", FullNameInviteUser);
            smailsubject = smailsubject.Replace("++campaignname++", CampaignName);


            MBody = MBody.Replace("++username++", Muser);
            MBody = MBody.Replace("++firstname++", Fname);
            MBody = MBody.Replace("++lastname++", Lname);
            MBody = MBody.Replace("++campaignname++", CampaignName);
            MBody = MBody.Replace("++rejectionreason++", RejectionReason);
            MBody = MBody.Replace("++campaignclicksperday++", CampaignClicksPerDay);
            MBody = MBody.Replace("++campaignvideopath++", CampaignVideoPath);
            MBody = MBody.Replace("++campaignvideoimage++", CampaignVideoImage);
            MBody = MBody.Replace("++campaignbannerimage++", CampaignBannerImage);
            MBody = MBody.Replace("++dealnoofdays++", DealNoOfDays);
            MBody = MBody.Replace("++userdealshtml++", UserDealsHTML);


            MBody = MBody.Replace("++ctlw++", ctlw);
            MBody = MBody.Replace("++ctpq++", ctpq);
            MBody = MBody.Replace("++trendc++", trendc);
            MBody = MBody.Replace("++alw++", alw);
            MBody = MBody.Replace("++apw++", apw);
            MBody = MBody.Replace("++trenda++", trenda);
            MBody = MBody.Replace("++clickthroughcolor++", clickthroughcolor);
            MBody = MBody.Replace("++answercolor++", answercolor);


            MBody = MBody.Replace("++winnerpoll++", winnerpoll);
            MBody = MBody.Replace("++winnerpollperc++", winnerpollperc);
            MBody = MBody.Replace("++pollanswercount++", pollanswercount);


            MBody = MBody.Replace("++paymentfailedattempt++", PaymentFailedAttempt);
            MBody = MBody.Replace("++paymentfailedreason++", PaymentFailedReason);
            MBody = MBody.Replace("++nextpaymentattempt++", NextPaymentAttempt);


            MBody = MBody.Replace("++reviewer++", Reviewer);
            MBody = MBody.Replace("++review++", Review);
            MBody = MBody.Replace("++reviewrating++", ReviewRating);

            MBody = MBody.Replace("++signuplocation++", SignupLocation);



            MBody = MBody.Replace("++CurrentDateTime++", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " GMT");
            MBody = MBody.Replace("++EmailConfirmationLink++", EmailConfirmationLink);
            MBody = MBody.Replace("++companyname++", CompanyNameInviteUser);
            MBody = MBody.Replace("++inviter++", FullNameInviteUser);

            MBody = MBody.Replace("++deleteaccountlink++", DeleteAccountLink);

            if (Mid == (int)EmailTypes.ResetPassword)
            {
                MBody = MBody.Replace("++PasswordResetLink++", PasswordResetLink);
            }
            if (Mid == (int)EmailTypes.Voucher)
            {
                MBody = MBody.Replace("++VoucherDescription++", VoucherDescription);
                MBody = MBody.Replace("++VoucherValue++", VoucherValue);
            }
            if (Mid == (int)EmailTypes.InviteUsers)
            {
                MBody = MBody.Replace("++companyname++", CompanyNameInviteUser);
                MBody = MBody.Replace("++inviteurl++", InviteURL);
                MBody = MBody.Replace("++rolename++", RoleName);
                MBody = MBody.Replace("++inviter++", FullNameInviteUser);
            }
            if (Mid == (int)EmailTypes.BuyItUsers)
            {
                MBody = MBody.Replace("++AdvertiserLogoURL++", AdvertiserLogoURL);
                MBody = MBody.Replace("++BuyItLogoURL++", BuyItLogoURL);
                MBody = MBody.Replace("++BuyItLine1++", BuyItLine1);
                MBody = MBody.Replace("++BuyItLine2++", BuyItLine2);
                MBody = MBody.Replace("++BuyItURL++", BuyItURL);
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

        private void SendEmailNotAysnc()
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
            smailsubject = smailsubject.Replace("++companyname++", CompanyNameInviteUser);
            smailsubject = smailsubject.Replace("++inviter++", FullNameInviteUser);
            smailsubject = smailsubject.Replace("++campaignname++", CampaignName);

            MBody = MBody.Replace("++username++", Muser);
            MBody = MBody.Replace("++firstname++", Fname);
            MBody = MBody.Replace("++lastname++", Lname);
            MBody = MBody.Replace("++campaignname++", CampaignName);
            MBody = MBody.Replace("++rejectionreason++", RejectionReason);
            MBody = MBody.Replace("++campaignclicksperday++", CampaignClicksPerDay);
            MBody = MBody.Replace("++campaignvideopath++", CampaignVideoPath);
            MBody = MBody.Replace("++campaignvideoimage++", CampaignVideoImage);
            MBody = MBody.Replace("++campaignbannerimage++", CampaignBannerImage);
            MBody = MBody.Replace("++dealnoofdays++", DealNoOfDays);
            MBody = MBody.Replace("++userdealshtml++", UserDealsHTML);


            MBody = MBody.Replace("++ctlw++", ctlw);
            MBody = MBody.Replace("++ctpq++", ctpq);
            MBody = MBody.Replace("++trendc++", trendc);
            MBody = MBody.Replace("++alw++", alw);
            MBody = MBody.Replace("++apw++", apw);
            MBody = MBody.Replace("++trenda++", trenda);
            MBody = MBody.Replace("++clickthroughcolor++", clickthroughcolor);
            MBody = MBody.Replace("++answercolor++", answercolor);

            MBody = MBody.Replace("++winnerpoll++", winnerpoll);
            MBody = MBody.Replace("++winnerpollperc++", winnerpollperc);
            MBody = MBody.Replace("++pollanswercount++", pollanswercount);

            MBody = MBody.Replace("++CurrentDateTime++", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " GMT");
            MBody = MBody.Replace("++EmailConfirmationLink++", EmailConfirmationLink);
            MBody = MBody.Replace("++inviteurl++", InviteURL);
            MBody = MBody.Replace("++fname++", Fname);
            MBody = MBody.Replace("++phone++", PhoneNo);
            MBody = MBody.Replace("++inviter++", FullNameInviteUser);
            MBody = MBody.Replace("++feedback++", feedback);
            MBody = MBody.Replace("++deleteaccountlink++", DeleteAccountLink);
            MBody = MBody.Replace("++countryname++", CountryName);

            MBody = MBody.Replace("++paymentfailedattempt++", PaymentFailedAttempt);
            MBody = MBody.Replace("++paymentfailedreason++", PaymentFailedReason);
            MBody = MBody.Replace("++nextpaymentattempt++", NextPaymentAttempt);


            MBody = MBody.Replace("++reviewer++", Reviewer);
            MBody = MBody.Replace("++review++", Review);
            MBody = MBody.Replace("++reviewrating++", ReviewRating);

            MBody = MBody.Replace("++signuplocation++", SignupLocation);

            MBody = MBody.Replace("++city++", City);


            if (Mid == (int)EmailTypes.ResetPassword)
            {
                MBody = MBody.Replace("++PasswordResetLink++", PasswordResetLink);
            }
            if (Mid == (int)EmailTypes.Voucher)
            {
                MBody = MBody.Replace("++VoucherDescription++", VoucherDescription);
                MBody = MBody.Replace("++VoucherValue++", VoucherValue);
            }
            if (Mid == (int)EmailTypes.InviteUsers)
            {
                MBody = MBody.Replace("++companyname++", CompanyNameInviteUser);
                MBody = MBody.Replace("++inviteurl++", InviteURL);
                MBody = MBody.Replace("++rolename++", RoleName);
            }
            if (Mid == (int)EmailTypes.BuyItUsers)
            {
                MBody = MBody.Replace("++AdvertiserLogoURL++", AdvertiserLogoURL);
                MBody = MBody.Replace("++BuyItLogoURL++", BuyItLogoURL);
                MBody = MBody.Replace("++BuyItLine1++", BuyItLine1);
                MBody = MBody.Replace("++BuyItLine2++", BuyItLine2);
                MBody = MBody.Replace("++BuyItURL++", BuyItURL);
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
                objSmtpClient.Send(oMailBody);
            }
            catch (Exception e)
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
        private readonly IManageUserRepository manageUserRepository;
        private readonly ICompanyService companyService;
        private readonly IAspnetUsersRepository userRepository;

        private readonly ICouponRepository couponRepository;


        #endregion


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EmailManagerService(ISystemMailsRepository systemMailRepository, IManageUserRepository manageUserRepository, ICompanyService companyService, IAspnetUsersRepository userRepository, ICouponRepository couponRepository)
        {

            if (systemMailRepository == null)
            {
                throw new ArgumentNullException("systemMailRepository");
            }
            this.systemMailRepository = systemMailRepository;
            this.manageUserRepository = manageUserRepository;
            this.companyService = companyService;
            this.userRepository = userRepository;
            this.couponRepository = couponRepository;


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
                Mid = (int)EmailTypes.VerifyAccount;
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
                SendEmailNoAsync();
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


        /// <summary>
        /// Send Password Reset Email
        /// </summary>
        public async Task SendDeleteAccountConfirmationEmail(User oUser, string deleteTokenLink)
        {
            MMailto.Add(oUser.Email);
            Mid = (int)EmailTypes.DeleteAccountConfirmationEmail;
            Muser = oUser.Email;
            Fname = oUser.FullName;
            PhoneNo = oUser.PhoneNumber;
            DeleteAccountLink = deleteTokenLink;
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
                Mid = (int)EmailTypes.PayoutNotificationToUser;
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
        ///Invite User Email
        /// </summary>
        public void SendEmailInviteToUserManage(string email, string InvitationCode, bool mode, string role)
        {
            MMailto.Add(email);
            Mid = (int)EmailTypes.InviteUsers;
            string userName = string.Empty;
            int companyid = 0;

            CompanyNameInviteUser = manageUserRepository.getCompanyName(out userName, out companyid);
            FullNameInviteUser = userName;
            RoleName = role;
            Muser = userName;

            if (mode == true)//user will have to register and a new user will be created etc and link established,.
                InviteURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Account/Register?code=" + InvitationCode;
            else//simple link acceptance logic since user already exists.
                InviteURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Account/AcceptInvitation?code=" + InvitationCode;


            SendEmailNotAysnc();



        }
        ////// invite user from mobile api 
        ////public async Task SendEmailToInviteUser(string email,int companyId)
        ////{

        ////    MMailto.Add(email);
        ////    Mid = (int)EmailTypes.InviteUsers;
        ////    string userName = string.Empty;

        ////    CompanyNameInviteUser = manageUserRepository.getCompanyName(out userName, companyId);
        ////    Muser = userName;
        ////    InviteURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Account/Register?CompanyID=" + companyId;
        ////    await SendEmail();

        ////}




        public async Task SendEmailInviteBusiness(string email, int companyId)
        {

            MMailto.Add(email);
            Mid = (int)EmailTypes.InviteBusiness;
            string userName = string.Empty;

            //CompanyNameInviteUser = manageUserRepository.getCompanyName(out userName, companyId);
            var company = companyService.GetCompanyById(companyId);
            CompanyNameInviteUser = company.CompanyName;

            Muser = userName;
            InviteURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Account/RegisterBusiness?ReferralCode=" + company.ReferralCode;
            SendEmailNotAysnc();

        }


        public async Task SendEmailInviteAdvertiser(string email, int companyId)
        {

            MMailto.Add(email);
            Mid = (int)EmailTypes.InviteAdvertiser;
            string userName = string.Empty;
            var company = companyService.GetCompanyById(companyId);
            CompanyNameInviteUser = CompanyNameInviteUser = company.CompanyName;
            Muser = userName;
            InviteURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Account/RegisterAdvertiser?ReferralCode=" + company.ReferralCode;
            SendEmailNotAysnc();

        }





        /// <summary>
        ///BuyIT User Email
        /// </summary>
        public async Task SendBuyItEmailToUser(string aspnetUserId, AdCampaign oCampaign)
        {
            User oUser = await UserManager.FindByIdAsync(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.BuyItUsers;
                Muser = oUser.FullName;
                AdvertiserLogoURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + "";
                BuyItLine1 = oCampaign.BuuyItLine1;
                BuyItLine2 = oCampaign.BuyItLine2;
                BuyItLogoURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + oCampaign.BuyItImageUrl;
                BuyItURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + oCampaign.LandingPageVideoLink;
                await SendEmail();
            }
            else
            {
                throw new Exception("Customer is null");
            }
        }
        public void SendVideoAdCampaignApprovalEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string videoPath, string videoImage)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.VideoAdCampaignApproved;
                Muser = oUser.FullName;

                CampaignName = campaignName;

                CampaignClicksPerDay = ClicksPerDay.ToString();
                CampaignVideoPath = videoPath;
                CampaignVideoImage = videoImage;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }
        public void SendVideoAdCampaignRejectionEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string videoPath, string videoImage, string RReason)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);

                Mid = (int)EmailTypes.VideoAdCampaignReject;


                Muser = oUser.FullName;
                RejectionReason = RReason;
                CampaignName = campaignName;
                CampaignClicksPerDay = ClicksPerDay.ToString();
                CampaignVideoPath = videoPath;
                CampaignVideoImage = videoImage;
                RejectionReason = RReason;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }



        public void SendSurveyCampaignApprovalEmail(string aspnetUserId, string campaignName, string LeftImage, string RightImage)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.PicturePollCampaignApproved;
                Muser = oUser.FullName;

                CampaignName = campaignName;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }

        public void SendSurveyCampaignRejectedEmail(string aspnetUserId, string campaignName, string LeftImage, string RightImage, string RReason)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.PicturePollCampaignRejected;
                Muser = oUser.FullName;

                CampaignName = campaignName;
                RejectionReason = RReason;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }


        public void SendDisplayAdCampaignApprovalEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string BannerPath)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.DisplayAdCampaignApproved;
                Muser = oUser.FullName;

                CampaignName = campaignName;

                CampaignClicksPerDay = ClicksPerDay.ToString();
                CampaignBannerImage = BannerPath;


                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }
        public void SendDisplayAdCampaignRejectionEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string BannerPath, string RReason)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);

                Mid = (int)EmailTypes.DisplayAdCampaignRejected;


                Muser = oUser.FullName;
                RejectionReason = RReason;
                CampaignName = campaignName;
                CampaignClicksPerDay = ClicksPerDay.ToString();
                CampaignBannerImage = BannerPath;
                RejectionReason = RReason;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }



        public void SendCouponCampaignApprovalEmail(string aspnetUserId, string campaignName, int dealNoOfDays, string BannerPath)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.CouponApproved;
                Muser = oUser.FullName;
                CampaignName = campaignName;
                CampaignBannerImage = BannerPath;
                DealNoOfDays = dealNoOfDays.ToString();

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }

        public void SendCouponCampaignRejectionEmail(string aspnetUserId, string campaignName, int dealNoOfDays, string BannerPath, string RReason)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.CouponRejected;
                Muser = oUser.FullName;
                CampaignName = campaignName;
                CampaignBannerImage = BannerPath;
                DealNoOfDays = dealNoOfDays.ToString();
                RejectionReason = RReason;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }

        public void SendProfileQuestionRejectionEmailForApproval(string aspnetUserId, string RReason)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.PicturePollCampaignRejected;
                Muser = oUser.FullName;
                RejectionReason = RReason;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }



        /// <summary>
        ///Invite User Email
        /// </summary>
        public void SendAppFeedback(string UserId, string feedback, string City, string Country, string FullName, string email, string phone)
        {
            MMailto.Add("info@cash4ads.com");
            Mid = (int)EmailTypes.AppFeedbackFromUser;
            string userName = email;


            this.Fname = FullName;
            this.PhoneNo = phone;
            this.sfemail = email;
            this.feedback = feedback;
            this.CountryName = Country;
            this.City = City;

            Muser = FullName;

            SendEmailNotAysnc();



        }

        //public async Task SendEmailToInviteUser(string email, string UserId)
        //{
        //    var oUser = manageUserRepository.GetByUserId(UserId);

        //    if (oUser != null)
        //    {
        //        MMailto.Add(email);
        //        Mid = (int)EmailTypes.InviteUsers;

        //        int companyid = 0;
        //        Muser = oUser.FullName;
        //        InviteURL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Account/Register?CompanyID=" + oUser.CompanyId;
        //        SendEmailNotAysnc();
        //    }


        //}
        #endregion



        public void SendPaymentRejectionEmail(string aspnetUserId, int CompanyId, string sPaymentFailedReason, int Attempt, string sNextPaymentAttempt)
        {
            var oUser = manageUserRepository.GetByUserId(aspnetUserId);
            var comp = companyService.GetCompanyById(CompanyId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.SubscriptionPaymentFailed;
                CompanyName = comp.CompanyName;
                Muser = oUser.FullName;
                PaymentFailedAttempt = Attempt.ToString();
                PaymentFailedReason = sPaymentFailedReason;
                NextPaymentAttempt = sNextPaymentAttempt;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }


        public bool SendCouponSubscriptionCreatedEmail(int companyId)
        {
            var comp = companyService.GetCompanyById(companyId);

            var oUser = userRepository.GetUserbyCompanyId(companyId);

            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.SubscriptionCreated;
                CompanyName = comp.CompanyName;
                Muser = oUser.FullName;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }

            return true;

        }


        public void SendCampaignPerformanceEmails()
        {

            var data = userRepository.GetCampaignPerformanceWeeklyStats();//new deals for today


            foreach (var campaign in data)
            {
                MMailto.Clear();

                MMailto.Add(campaign.email);

                CampaignName = campaign.CampaignName;

                switch (campaign.type)
                {
                    case 1:
                        {
                            Mid = (int)EmailTypes.WeeklyVideoAdPerformanceStats;

                            ctlw = campaign.ClickThroughsLastWeek.ToString();
                            ctpq = campaign.ClickThroughsPreviousWeek.ToString();
                            trendc = campaign.ProgressPercentage.ToString();

                            if (campaign.ProgressPercentage > 0)
                                clickthroughcolor = "green";
                            else
                                clickthroughcolor = "red";


                            alw = campaign.AnsweredLastWeek.ToString();
                            apw = campaign.AnsweredPreviousWeek.ToString();
                            trenda = campaign.ProgressPercentageAnswer.ToString();

                            if (campaign.ProgressPercentageAnswer > 0)
                                answercolor = "green";
                            else
                                answercolor = "red";


                            break;
                        }
                    case 4:
                        {
                            Mid = (int)EmailTypes.WeeklyDisplayAdPerformanceStats;

                            ctlw = campaign.ClickThroughsLastWeek.ToString();
                            ctpq = campaign.ClickThroughsPreviousWeek.ToString();
                            trendc = campaign.ProgressPercentage.ToString();

                            if (campaign.ProgressPercentage > 0)
                                clickthroughcolor = "green";
                            else
                                clickthroughcolor = "red";


                            alw = campaign.AnsweredLastWeek.ToString();
                            apw = campaign.AnsweredPreviousWeek.ToString();
                            trenda = campaign.ProgressPercentageAnswer.ToString();

                            if (campaign.ProgressPercentageAnswer > 0)
                                answercolor = "green";
                            else
                                answercolor = "red";


                            break;
                        }
                    case 5:
                        {
                            Mid = (int)EmailTypes.WeeklyDealPerformanceStats;

                            ctlw = campaign.ClickThroughsLastWeek.ToString();
                            ctpq = campaign.ClickThroughsPreviousWeek.ToString();
                            trendc = campaign.ProgressPercentage.ToString();

                            if (campaign.ProgressPercentage > 0)
                                clickthroughcolor = "green";
                            else
                                clickthroughcolor = "red";


                            alw = campaign.AnsweredLastWeek.ToString();
                            apw = campaign.AnsweredPreviousWeek.ToString();
                            trenda = campaign.ProgressPercentageAnswer.ToString();

                            if (campaign.ProgressPercentageAnswer > 0)
                                answercolor = "green";
                            else
                                answercolor = "red";


                            break;
                        }
                    case 6:
                        {
                            Mid = (int)EmailTypes.WeeklyPollSurveyPerformanceStats;


                            if (campaign.LeftPicResponseCount > campaign.RightPicResponseCount)
                            {
                                winnerpoll = "One";
                                winnerpollperc = ((campaign.LeftPicResponseCount - campaign.RightPicResponseCount) / campaign.RightPicResponseCount * 100).ToString() + "%";
                            }
                            else
                            {
                                winnerpoll = "Two";
                                winnerpollperc = ((campaign.RightPicResponseCount - campaign.LeftPicResponseCount) / campaign.LeftPicResponseCount * 100).ToString() + "%";
                            }

                            pollanswercount = (campaign.LeftPicResponseCount + campaign.RightPicResponseCount).ToString();


                            break;
                        }
                }

                Muser = campaign.FullName;

                SendEmailNotAysnc();

            }


        }

        public void SendNewDealsEmail(int mode)
        {

            var data = couponRepository.GetUsersCouponsForEmailNotification(mode);//new deals for today


            if (data != null && data.Count() > 0)
            {
                string userDeals = string.Empty;

                var users = data.GroupBy(x => new { x.UserId, x.FullName, x.Email });

                foreach (var user in users)
                {
                    userDeals = "<table align=\"center\" bgcolor=\"#F2F2F2\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style='width:100%;'>";
                    //user.Key.UserId

                    foreach (var item in user)
                    {
                        userDeals += "<tr><td colspan='2' align=\"center\"><img style='text-align:center;max-width:560px' src='" + item.couponimage1 + "'/></td></tr>";
                        userDeals += "<tr><td colspan='2' align=\"center\" style='text-align:center;padding-bottom:10px;'><p style=\"style=color:#737373; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;\">" + item.CouponTitle + " <span style='color:red'>" + item.CurrencySymbol + "" + item.SavingsNew + "</span></p></td></tr>";
                        userDeals += "<tr><td style='padding-left:64px;padding-bottom:60px'><p style=\"style=color:red; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:700; line-height:24px; padding-top:0; margin-top:0; text-align:left;\">was " + item.CurrencySymbol + "" + item.price + "</p></td><td align='right' style='padding-right:60px'><a href='http://deals.cash4ads.com/deal/" + item.CouponId + "' style=\"background-color:#6DC6DD; border-collapse:separate; border-top:20px solid #6DC6DD; border-right:40px solid #6DC6DD; border-bottom:20px solid #6DC6DD; border-left:40px solid #6DC6DD; border-radius:3px; color:#FFFFFF; display:inline-block; font-family:'Helvetica Neue', Helvetica, Arial, sans-serif; font-size:16px; font-weight:600; letter-spacing:.3px; text-decoration:none;\" target='_blank'>VIEW DEAL</a></td></tr>";
                    }


                    userDeals += "</table>";

                    MMailto.Clear();

                    MMailto.Add(user.Key.Email);
                    if (mode == 1)
                        Mid = (int)EmailTypes.NewCouponsNearMe;
                    else if (mode == 2)
                        Mid = (int)EmailTypes.Last3DaysPercentageCouponsNearMe;
                    else if (mode == 3)
                        Mid = (int)EmailTypes.Last2DaysPercentageCouponsNearMe;
                    else if (mode == 4)
                        Mid = (int)EmailTypes.LastDayPercentageCouponsNearMe;
                    else if (mode == 5)
                        Mid = (int)EmailTypes.Last3DaysDollarDiscountCouponsNearMe;
                    else if (mode == 6)
                        Mid = (int)EmailTypes.Last2DaysDollarDiscountCouponsNearMe;
                    else if (mode == 7)
                        Mid = (int)EmailTypes.LastDayDollarDiscountCouponsNearMe;


                    Muser = user.Key.FullName;
                    UserDealsHTML = userDeals;


                    SendEmailNotAysnc();

                }
            }


        }


        public void previewEmail(int mailid, string email)
        {
            //var comp = companyService.GetCompanyById(companyId);

            //var oUser = userRepository.GetUserbyem(companyId);


            MMailto.Add(email);
            Mid = mailid;
            CompanyName = "Preview Company";
            Muser = "Preview User";
            CountryName = "Preview Country";
            PhoneNo = "+92 333 416 8877";
            BillingMonth = "month";
            DueDate = "due date ?";
            CustomerAccountNo = "customer Accoutn no ";
            TotalAmount = "Total Amount";
            InVoiceCode = "Invoice Code";
            ReceiptBody = "Receipt Body";
            EmailConfirmationLink = "http://cash4ads.com/confirm";
            PasswordResetLink = "http://cash4ads.com/reset";

            DeleteAccountLink = "http://cash4ads.com/delete";

            CompanyNameInviteUser = "Mz Inviter comp";
            FullNameInviteUser = "mz Inviter";
            InviteURL = "http://cash4ads.com/invite";

            CampaignName = "Campaign XX";
            CampaignClicksPerDay = "999";
            CampaignVideoPath = "http://cash4ads.com/videopath";
            CampaignVideoImage = "http://cash4ads.com/campaignimage";
            CampaignBannerImage = "http://cash4ads.com/bannerpath";
            RejectionReason = "damn it the reason is not available ";
            PaymentFailedReason = "Payment failed ressonnn";
            PaymentFailedAttempt = "99xx";
            NextPaymentAttempt = "Very very soon";
            RoleName = "Role name here";



            SendEmailNotAysnc();



        }


        public void SendNewReviewAvailableToAdvertiser(string ReviewerUserId, string campaignName, double Rating, string Reviewtext, string ReviwerFullName, string AdvertiserUserId)
        {
            var oUser = manageUserRepository.GetByUserId(AdvertiserUserId);


            if (oUser != null)
            {
                MMailto.Add(oUser.Email);
                Mid = (int)EmailTypes.DealReviewNotificationToAdvertiser;
                Muser = oUser.FullName;
                CampaignName = campaignName;
                Reviewer = ReviwerFullName;
                ReviewRating = Rating.ToString();
                Review = Reviewtext;

                SendEmailNotAysnc();
            }
            else
            {
                throw new Exception("Email could not be sent!");
            }
        }



        /// <summary>
        ///Invite User Email
        /// </summary>
        public void NewUserSignupToAdmin(string UserId, string FullName, string email, string phone, string signuplocation)
        {
            MMailto.Add("info@cash4ads.com");
            Mid = (int)EmailTypes.NewUserSignupToAdmin;
            string userName = email;


            this.Fname = FullName;
            this.PhoneNo = phone;
            this.sfemail = email;
            SignupLocation = signuplocation;

            Muser = FullName;

            SendEmailNotAysnc();



        }


        public void SendDealExpiredNotificationToAdvertiser()
        {


            var data = couponRepository.GetDealsWhichHavejustExpired();
            try
            {
                foreach (var item in data)
                {
                    MMailto.Clear();

                    MMailto.Add(item.Email);

                    Mid = (int)EmailTypes.DealExpiryNotificationToAdvertiser;

                    Muser = item.FullName;
                    CampaignName = item.CouponTitle;
                    DealNoOfDays =   item.DaysLeft.Value.ToString();
                    SendEmailNotAysnc();
                }
            }
            catch (Exception e)
            {
                // logg the exception
            }

            var couponsToComplete = data.Select(g => g.CouponId).ToArray();
            couponRepository.CompleteCoupons(couponsToComplete);
        }

    }




}
