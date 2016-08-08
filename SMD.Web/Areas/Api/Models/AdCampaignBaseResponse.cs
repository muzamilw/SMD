using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class AdCampaignBaseResponse
    {
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<LanguageDropdown> Languages { get; set; }
        public IEnumerable<CurrencyDropdown> Currencies { get; set; }
        /// <summary>
        /// User and Cost detail
        /// </summary>
        public UserAndCostDetail UserAndCostDetails { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public IEnumerable<LocationDropDown> countriesAndCities { get; set; }

        /// <summary>
        /// profile question
        /// </summary>
        public IEnumerable<ProfileQuestionDropdown> ProfileQuestions { get; set; }

        /// <summary>
        /// profile question answer
        /// </summary>
        public IEnumerable<ProfileQuestionAnswerDropdown> ProfileQuestionAnswers { get; set; }

        /// <summary>
        /// survey question
        /// </summary>
        public IEnumerable<SurveyQuestionDropDown> SurveyQuestions { get; set; }

        public IEnumerable<Industry> listIndustry { get; set; }
        public IEnumerable<Education> listEducation { get; set; }

        /// <summary>
        /// Industory
        /// </summary>
        public IEnumerable<Models.Industry> Professions { get; set; }
        /// <summary>
        /// Education
        /// </summary>
        public IEnumerable<Models.Education> Educations { get; set; }
        public IEnumerable<AdCampaignDropDown> AdCampaigns { get; set; }
        public IEnumerable<CouponCategoryModel> CouponCategories { get; set; }
        
        public IEnumerable<DiscountVoucher> DiscountVouchers { get; set; }
        public IEnumerable<CompanyBranch> listBranches { get; set; }
    }
}