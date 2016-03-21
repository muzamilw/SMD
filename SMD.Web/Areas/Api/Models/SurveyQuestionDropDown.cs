using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class SurveyQuestionDropDown
    {
        public long SQID { get; set; }
        public string DisplayQuestion { get; set; }
        public string LeftPicturePath { get; set; }
        public string RightPicturePath { get; set; }
    }
    public class AdCampaignDropDown
    {
        public long CampaignId { get; set; }
        public string VerifyQuestion { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
    }
}