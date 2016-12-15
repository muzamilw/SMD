namespace SMD.Models.Common
{
    public enum HashAlgos
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    
    
    public enum CompanyType
    {
        User = 2,
        SMD = 1
    }
    public enum RequestType
    {
        Xml = 0, 
        Json = 1 
    }

    /// <summary>
    /// System Email Types 
    /// </summary>
    public enum EmailTypes
    {
        VerifyAccount = 6, // Email Confirmation
        ResetPassword = 9,
        RegistrationConfirmed = 1,
        QuestionApproved = 10,
        QuestionRejected = 11,
        CollectionMade=12,
        PayoutNotificationToUser=13,
        Voucher = 14,
       InviteUsers = 15,
       BuyItUsers = 16,
       VoucherPaymentEmail = 14,
       VideoAdCampaignApproved = 17,
       VideoAdCampaignReject = 18,
       PayoutNotificationToAdmin = 19,
        InviteBusiness = 20,
        InviteAdvertiser = 21,
        AppFeedbackFromUser = 22,
        SubscriptionPaymentFailed = 23,
        DeleteAccountConfirmationEmail = 24,
        SubscriptionCreated = 25,
        CouponApproved = 26,
        CouponRejected = 27,
        DisplayAdCampaignApproved = 28,
        DisplayAdCampaignRejected = 29,
        PicturePollCampaignApproved = 30,
        PicturePollCampaignRejected = 31,
        NewCouponsNearMe = 32,
        Last3DaysPercentageCouponsNearMe = 33,
        Last2DaysPercentageCouponsNearMe = 34,
        LastDayPercentageCouponsNearMe = 35,
        Last3DaysDollarDiscountCouponsNearMe = 36,
        Last2DaysDollarDiscountCouponsNearMe = 37,
        LastDayDollarDiscountCouponsNearMe = 38,
        WeeklyVideoAdPerformanceStats = 39,
        WeeklyDisplayAdPerformanceStats = 40,
        WeeklyDealPerformanceStats = 41,
        WeeklyPollSurveyPerformanceStats = 42,
        DealReviewNotificationToAdvertiser = 43,
        NewUserSignupToAdmin = 44,
        DealExpiryNotificationToAdvertiser = 45
    }

    public enum AdCampaignCriteriaType
    {
        ProfileQuestion = 1,
        SurveyQuestion = 2,
        Language = 3,
        QuizQustion = 6
    }
    public enum AdCampaignType
    {
        Video = 1,
        Link = 2,
        Other = 3,
        Coupon = 5
    }

    public enum AdCampaignStatus 
    {
        Draft = 1,
        SubmitForApproval = 2,
        Live = 3,
        Paused = 4,
        Completed = 5,
        ApprovalRejected = 6,
        Remove = 7,
        Archived = 8,
        AutoComplete = 9
    }
    /// <summary>
    /// Object Status enum for profile question
    /// </summary>
    public enum ObjectStatus
    {
        Archived=0,
        Active =1
    }

    /// <summary>
    /// Audit Log Keys
    /// </summary>
    public enum AuditLogEntityType
    {
        /// <summary>
        /// All Entities with No Filter
        /// </summary>
        All = 0,

        /// <summary>
        /// Profile Question
        /// </summary>
        ProfileQuestion = 1,

        /// <summary>
        /// Survry Question
        /// </summary>
        SurvryQuestion = 2,

        /// <summary>
        /// Ad Campaign
        /// </summary>
        AdCampaign = 3
    }
    public enum SurveyQuestionAnswerType
    {
        Left = 1,
        Right = 2
    }
    public enum SurveyQuestionStatus
    {
        Draft = 1 ,
        SubmittedForApproval = 2,
        Live = 3,
        Paused = 4,
        Completed = 5,
        ApprovalRejected = 6
    }

    public enum UserStatus
    {
        Active = 1,
        InActive = 0
    }
    public enum SurveyQuestionType
    {
        Advertiser = 1,
        MySMD = 2
    }

    /// <summary>
    /// Account Names
    /// </summary>
    public static class Accounts
    {
        public static string Smd { get { return "SMD"; } }
    }

    /// <summary>
    /// System User
    /// </summary>
    public static class SystemUsers
    {
        public static string Cash4Ads { get { return "cash4ads@cash4ads.com"; } }
    }

    /// <summary>
    /// Transaction Types
    /// </summary>
    public enum TransactionType
    {
       
        AdClick = 1,
        ApproveSurvey = 2,
        ViewSurveyReport = 3,
        CouponPurchased = 4,
        SurveyWatched = 5,
        ProfileQuestionAnswered = 6,
        ApproveProfileQuestion = 7,
        ApproveCoupon = 8,
        ApproveAd = 9,
        UserCashOutPaypal = 10,
        AdWeeklyCollection = 11,
        WelcomeGiftBalance = 12,
        PromotionalCentz = 13,
        ReferFriendBalance = 14,
        UnCollectedCentz = 14


    }
    public enum SurveyQuestionTargetCriteriaType
    {
        ProfileQuestion = 1,
        UserProfileQuestion = 6, 
        SurveryQuestion = 2, 
        Language = 3,
        Industry = 4,
        Education = 5
    }
    public enum ProfileQuestionTargetCriteriaType
    {
        ProfileQuestion = 1,
        SurveryQuestion = 2,
        Language = 3,
        Industry = 4,
        Education = 5
    }
    public enum ProductCode
    {
        SurveyQuestion = 1,
        AdApproval = 2,
        DownloadReport = 3
    }

    /// <summary>
    /// User Account Types
    /// </summary>
    public enum AccountType
    {
        Stripe =1 ,
        Paypal=2,
        GoogleWallet=3,
        VirtualAccount=4
    }

    /// <summary>
    /// User payment mehtod  Types
    /// </summary>
    public enum PaymentMethod
    {
        Paypal = 1,
        Coupon = 3
    }
    public enum CampaignType
    {
        VideoAd = 1,
        GameAd = 4,
        SurveyQuestion = 2,
        ProfileQuestion = 3
    }


    public enum CampaignResponseEventType
    {
        Opened = 1,
        BuyItbuttonClicked = 2,
        Answered =3,
        Skip = 4,
        


    }


    /// <summary>
    /// Ad Reward
    /// </summary>
    public enum AdRewardType
    {
        Cash = 1,
        Voucher = 2
    }
}