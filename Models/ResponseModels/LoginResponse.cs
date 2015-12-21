using SMD.Models.IdentityModels;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Login Response 
    /// </summary>
    public class LoginResponse : BaseApiResponse
    {
       public User User { get; set; }
    }
}
