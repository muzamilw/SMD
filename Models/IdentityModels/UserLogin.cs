namespace SMD.Models.DomainModels
{
    /// <summary>
    /// User Login Domain Model
    /// </summary>
    public class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
