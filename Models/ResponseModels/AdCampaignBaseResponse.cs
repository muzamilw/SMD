using SMD.Models.Common;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.ResponseModels
{
    public class AdCampaignBaseResponse
    {
        /// <summary>
        /// Langs
        /// </summary>
        public IEnumerable<Language> Languages { get; set; }

        /// <summary>
        /// User and Cost detail
        /// </summary>
        public UserAndCostDetail UserAndCostDetails { get; set; }

        /// <summary>
        /// Cities
        /// </summary>
        public IEnumerable<City> Cities { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public IEnumerable<Country> countries { get; set; }

        /// <summary>
        /// profile questions
        /// </summary>
        public IEnumerable<ProfileQuestion> ProfileQuestions { get; set; }

        /// <summary>
        /// profile questions answers
        /// </summary>
        public IEnumerable<ProfileQuestionAnswer> ProfileQuestionAnswers { get; set; }

        /// <summary>
        /// survey questions
        /// </summary>
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
        /// <summary>
        /// industry
        /// </summary>
        public IEnumerable<Industry> Industry { get; set; }
        /// <summary>
        /// Education
        /// </summary>
        public IEnumerable<Education> Education { get; set; }

        public IEnumerable<AdCampaign> AdCampaigns { get; set; }

        public IEnumerable<CouponCategory> CouponCategory { get; set; }
    }
}
