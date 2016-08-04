using System;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Sp Results For Api
    /// </summary>
    public class GetSurveys_Result
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
