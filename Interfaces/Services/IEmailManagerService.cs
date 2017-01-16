using SMD.Models.DomainModels;
using SMD.Models.IdentityModels;
using System.Threading.Tasks;
using System.Web;

namespace SMD.Interfaces.Services
{
    /// <summary>
    /// Email Manager Interface
    /// </summary>
    public interface IEmailManagerService
    {
        /// <summary>
        /// Send Voucher to User
        /// </summary>
        Task SendVoucherEmail(string aspnetUserId, string voucherDescription, string voucherValue, string voucherImage);

        /// <summary>
        /// Send Account Verification Email
        /// </summary>
        Task SendAccountVerificationEmail(User oUser, string emailConfirmationLink);

        /// <summary>
        /// On Registration Success Email
        /// </summary>
        Task SendRegisrationSuccessEmail(string aspnetUserId);

        /// <summary>
        /// Send Password Email
        /// </summary>
        Task SendPasswordResetLinkEmail(User oUser, string passwordResetLink);


        Task SendDeleteAccountConfirmationEmail(User oUser, string deleteTokenLink);

        /// <summary>
        /// Send Error Log
        /// </summary>
        bool SendErrorLog(string errormsg);

        /// <summary>
        /// Send Invoice
        /// </summary>
        bool SendInovice();


        /// <summary>
        /// Send Email on Question Approval 
        /// </summary>
        Task SendQuestionApprovalEmail(string aspnetUserId);


        /// <summary>
        ///Send Email on Question Rejection 
        /// </summary>
        Task SendQuestionRejectionEmail(string aspnetUserId);


        /// <summary>
        ///Send Email when Collection scheduler run
        /// </summary>
        Task SendCollectionRoutineEmail(string aspnetUserId);

        /// <summary>
        ///Send Email when Payout scheduler run
        /// </summary>
        Task SendPayOutRoutineEmail(string aspnetUserId);
        void SendEmailInviteToUserManage(string email, string InvitationCode, bool mode, string RoleName);


        Task SendBuyItEmailToUser(string aspnetUserId, AdCampaign oCampaign);
        void SendVideoAdCampaignApprovalEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string videoPath, string videoImage);

        void SendVideoAdCampaignSubmissionEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string videoPath, string videoImage);

        void SendVideoAdCampaignRejectionEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string videoPath, string videoImage, string RReason);
       


        Task SendEmailInviteBusiness(string email, int companyId);
        Task SendEmailInviteAdvertiser(string email, int companyId);
        void SendProfileQuestionRejectionEmailForApproval(string aspnetUserId, string RReason);


        void SendAppFeedback(string UserId, string feedback, string City, string Country, string FullName, string email, string phone);

        void SendPaymentRejectionEmail(string aspnetUserId, int CompanyId, string sPaymentFailedReason, int Attempt,string NextPaymentAttempt);

        bool SendCouponSubscriptionCreatedEmail(int companyId);


        void SendSurveyCampaignApprovalEmail(string aspnetUserId, string campaignName, string LeftImage, string RightImage);
        void SendSurveyCampaignSubmissionEmail(string aspnetUserId, string campaignName, string LeftImage, string RightImage);
        void SendSurveyCampaignRejectedEmail(string aspnetUserId, string campaignName, string LeftImage, string RightImage, string RReason);


        void SendDisplayAdCampaignApprovalEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string BannerPath);
        void SendDisplayAdCampaignSubmissionEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string BannerPath);
        void SendDisplayAdCampaignRejectionEmail(string aspnetUserId, string campaignName, int ClicksPerDay, string BannerPath, string RReason);


        void SendCouponCampaignApprovalEmail(string aspnetUserId, string campaignName, int dealNoOfDays, string BannerPath);
        void SendCouponCampaignSubmissionEmail(string aspnetUserId, string campaignName, int dealNoOfDays, string BannerPath);
        void SendCouponCampaignRejectionEmail(string aspnetUserId, string campaignName, int dealNoOfDays, string BannerPath, string RReason);


        void SendNewDealsEmail(int mode);


        void SendCampaignPerformanceEmails();

        void previewEmail(int mailid, string email);

        void SendNewReviewAvailableToAdvertiser(string ReviewerUserId, string campaignName, double Rating, string Reviewtext, string ReviwerFullName, string AdvertiserUserId);


        void NewUserSignupToAdmin(string UserId, string FullName, string email, string phone, string signuplocation);



        void SendDealExpiredNotificationToAdvertiser();

        void Send3DaysDealExpiredNotificationToAdvertiser();

        void SendNotificationToAdvertiserForAdditional20PercentDiscounton3rdLastDay();

        void SendNotificationToAdvertiserForAdditional25PercentDiscounton2ndLastDay();

        void SendNotificationToAdvertiserForAdditional30PercentDiscountonLastDay();

        void SendNotificationToAdvertiserForAdditional10DollarDiscounton3rdLastDay();

        void SendNotificationToAdvertiserForAdditional20DollarDiscounton2ndLastDay();

        void SendNotificationToAdvertiserForAdditional30DollarDiscountonLastDay();

    }
}
