using System;

namespace SMD.MIS.Areas.Api.Models
{
    /// <summary>
    /// Survey Web Model | API 
    /// </summary>
    public class SurveyApiModel
    {
        public long SQID { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public string DisplayQuestion { get; set; }
        public string LeftPicturePath { get; set; }
        public string RightPicturePath { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string VoucherCode { get; set; }
        public long? ResultClicks { get; set; }  
    }
}