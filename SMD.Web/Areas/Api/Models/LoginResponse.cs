
namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Login Response WebApi Model
    /// </summary>
    public class LoginResponse
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public WebApiUser User { get; set; }
    }
}