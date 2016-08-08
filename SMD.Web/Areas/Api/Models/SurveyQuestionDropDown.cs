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
        public bool isSelected { get; set; }
    }
    public  class CompanyBranch
    {
      

        public long BranchId { get; set; }
        public string BranchTitle { get; set; }
        public string BranchAddressLine1 { get; set; }
        public string BranchAddressLine2 { get; set; }
        public string BranchCity { get; set; }
        public string BranchState { get; set; }
        public string BranchZipCode { get; set; }
        public string BranchPhone { get; set; }
        public string BranchLocationLat { get; set; }
        public string BranchLocationLong { get; set; }
        public Nullable<long> BranchCategoryId { get; set; }
        public Nullable<int> CompanyId { get; set; }

    }
}