using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class GenerateSMSResponse : BaseApiResponse
    {
        public string VerificationCode { get; set; }
        
    }
}