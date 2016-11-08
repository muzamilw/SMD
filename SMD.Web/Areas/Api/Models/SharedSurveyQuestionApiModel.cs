
using System;
namespace SMD.MIS.Areas.Api.Models
{



    public partial class SharedSurveyQuestionRequestApiModel
    {


        public long SSQID { get; set; }
        public string UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string SurveyTitle { get; set; }
        
        public string LeftPictureDataString { get; set; }
        
        public string RightPictureDataString { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }

        public long SharingGroupId { get; set; }
       

    }
    
}