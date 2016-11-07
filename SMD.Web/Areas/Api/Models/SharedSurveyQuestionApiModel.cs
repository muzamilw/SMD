
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
    public partial class SharedSurveyQuestionResponseApiModel
    {
       
    
        public long SSQID { get; set; }
        public string UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string SurveyTitle { get; set; }
        public string LeftPicturePath { get; set; }
        public string RightPicturePath { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }

        public long SharingGroupId { get; set; }
        public string GroupName { get; set; }


        public int LeftPictureResultPercentage { get; set; }


        public int LeftPictureResultMaleCount { get; set; }

        public int LeftPictureResultFemaleCount { get; set; }
        public int RightPictureResultPercentage { get; set; }
        public int RightPictureResultMaleCount { get; set; }

        public int RightPictureResultFemaleCount { get; set; }

        public int TotalAnswered { get; set; }
        
        
    
    }
}