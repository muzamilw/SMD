using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class CampaignRequestResponseModel
    {
        public IEnumerable<AdCampaign> Campaigns { get; set; }
        public IEnumerable<Coupon> Coupon { get; set; }
        /// <summary>
        /// Total Count of  Profile Questions
        /// </summary>
        public int TotalCount { get; set; }
        public IEnumerable<SearchCampaigns> CampaignsList { get; set; }
    }
}