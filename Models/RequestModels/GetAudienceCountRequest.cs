using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.RequestModels
{
    public class GetAudienceCountRequest
    {
        public int ageFrom {get;set;}
        public int ageTo {get;set;}
        public int gender {get;set;}
        public string countryIds { get; set; }
        public string cityIds { get; set; }
        public string languageIds { get; set; }
        public string industryIds { get; set; }
        public string profileQuestionIds { get; set; }
        public string profileAnswerIds { get; set; }
        public string surveyQuestionIds { get; set; }
        public string surveyAnswerIds { get; set; }
        public string countryIdsExcluded { get; set; }
        public string cityIdsExcluded { get; set; }
        public string languageIdsExcluded { get; set; }
        public string industryIdsExcluded { get; set; }
        public string profileQuestionIdsExcluded { get; set; }
        public string profileAnswerIdsExcluded { get; set; }
        public string surveyQuestionIdsExcluded { get; set; }
        public string surveyAnswerIdsExcluded { get; set; }
    }
}
