using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.Common
{
    public class UserAndCostDetail
    {
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? LanguageId { get; set; }
        public int? IndustryId { get; set; }
        public long? EducationId { get; set; }
        public double AgeClausePrice { get; set; }
        public double GenderClausePrice { get; set; }
        public double LocationClausePrice { get; set; }
        public double OtherClausePrice { get; set; }
        public double ProfessionClausePrice { get; set; }
        public double EducationClausePrice { get; set; }
        public double BuyItClausePrice { get; set; }

        public double QuizQuestionClausePrice { get; set; }
        public double TenDayDeliveryClausePrice { get; set; }
        public double FiveDayDeliveryClausePrice { get; set; }
        public double ThreeDayDeliveryClausePrice { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string LanguageName { get; set; }
        public string IndustryName { get; set; }
        public string EducationTitle { get; set; }
        public bool isStripeIntegrated { get; set; }

        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
    }
}
