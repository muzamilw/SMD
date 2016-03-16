﻿namespace SMD.Models.Common
{
    public enum HashAlgos
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    
    /// <summary>
    /// Roles
    /// </summary>
    public static class Roles
    {
        public static string Adminstrator { get { return "AMD Admin"; } }
        public static string Approver { get { return "SMD Approver"; } }
        public static string Editor { get { return "SMD Editor"; } }
        public static string User { get { return "User"; } }
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
        PayoutMade=13,
        Voucher = 14,
       InviteUsers = 15
    }

    public enum AdCampaignCriteriaType
    {
        ProfileQuestion = 1,
        SurveyQuestion = 2,
        Language = 3
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
        TerminatedByUser = 7,
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
        /// <summary>
        /// Ad Click / Viewed
        /// </summary>
        AdClick = 1,

        /// <summary>
        /// Approve Survey
        /// </summary>
        ApproveSurvey = 2,

        /// <summary>
        /// View Survey Report
        /// </summary>
        ViewSurveyReport = 3
    }
    public enum SurveyQuestionTargetCriteriaType
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
    public enum ProductType
    {
        Ad = 1,
        SurveyQuestion = 2,
        Question = 3
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