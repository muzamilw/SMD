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
}