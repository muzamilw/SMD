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

        /// <summary>
        /// Country
        /// </summary>
        public IEnumerable<CountryDropdown> countries { get; set; }
    }
}