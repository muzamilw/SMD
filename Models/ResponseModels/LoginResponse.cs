using SMD.Models.IdentityModels;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Login Response 
    /// </summary>
    public class LoginResponse
    {
       public bool Status { get; set; }
        
       public string Message { get; set; }

       public User User { get; set; }
    }
}
