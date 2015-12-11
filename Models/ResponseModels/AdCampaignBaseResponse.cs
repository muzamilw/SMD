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
    }
}
