
using System;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Login Response WebApi Model
    /// </summary>
    public class LoginResponse : BaseApiResponse
    {
        public WebApiUser User { get; set; }

        public Guid AuthenticationToken { get; set; }
    }
}