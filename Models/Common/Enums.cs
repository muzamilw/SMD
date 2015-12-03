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
        RegisterConfirm = 5,
        VerifyAccount = 6, // Email Confirmation
        SuuportTicketReciept = 7,
        TicketAssignedToUser = 12,
        IssueResolved = 13,
        TicketReAssignedToUser = 14,
        DeveloperDueDate = 15,
        DeveloperQuestion = 16,
        ResetPassword = 9,
        RegistrationConfirmed = 1
    }

    public enum AdvertType
    {
        Video = 1,
        Link = 2,
        Other = 3
    }

    public enum AdvertStatus 
    {
        Draft = 1,
        SubmitForApproval = 2,
        Live = 3,
        Paused = 4,
        Completed = 5,
        ApprovalRejected = 6
    }
}