namespace SMD.Models.DomainModels
{
    /// <summary>
    /// System Mail Domain Model
    /// </summary>
    public class SystemMail
    {
        public int MailId { get; set; }
        public string Title { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int? EmailTarget { get; set; }
    }
}
