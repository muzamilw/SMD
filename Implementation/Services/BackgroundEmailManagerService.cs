using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SMD.ExceptionHandling;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using SMD.Repository.BaseRepository;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Email Manager Service
    /// </summary>
    public class BackgroundEmailManagerService
    {
        #region private

        /// <summary>
        /// Sends Email
        /// </summary>
        private static void SendEmail(SystemMail email, List<string> mMailto)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            MailMessage oMailBody = new MailMessage();
            // ReSharper restore SuggestUseVarKeywordEvident
            
            if (email == null)
            {
                throw new ArgumentException("email");
            }


            oMailBody.IsBodyHtml = true;
            oMailBody.From = new MailAddress(email.FromEmail, email.FromName);
            oMailBody.Subject = email.Subject;
            oMailBody.Body = email.Body;

            oMailBody.To.Clear();
            foreach (var elememnt in mMailto)
            {
                oMailBody.To.Add(new MailAddress(elememnt, email.FromName));
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
            mMailto.Clear();
            try
            {
                objSmtpClient.Send(oMailBody);
            }
            catch (Exception)
            {
                throw new SMDException(LanguageResources.EmailManagerService_FailedToSendEmail);
            }
        }


        #endregion


        #region Constructor

        #endregion

        #region Public

        // ReSharper restore SuggestUseVarKeywordEvident

        /// <summary>
        /// Send Email when Collection scheduler run
        /// </summary>
        public static void SendCollectionRoutineEmail(BaseDbContext context, string aspnetUserId)
        {
            User oUser = context.Users.FirstOrDefault(user => user.Id == aspnetUserId);
            SystemMail mail = context.SystemMails.FirstOrDefault(email => email.MailId == (int)EmailTypes.CollectionMade);

            if (oUser != null && mail != null)
            {
                SendEmail(mail, new List<string> { oUser.Email });
            }
            else
            {
                throw new Exception("Customer is null");
            }
        }


        /// <summary>
        /// Send Email when Payout scheduler run
        /// </summary>
        public static void SendPayOutRoutineEmail(BaseDbContext context, string aspnetUserId)
        {
            User oUser = context.Users.FirstOrDefault(user => user.Id == aspnetUserId);
            SystemMail mail = context.SystemMails.FirstOrDefault(email => email.MailId == (int)EmailTypes.PayoutMade);

            if (oUser != null && mail != null)
            {
                SendEmail(mail, new List<string> { oUser.Email });
            }
            else
            {
                throw new Exception("Customer is null");
            }
        }
        #endregion

    }
}
