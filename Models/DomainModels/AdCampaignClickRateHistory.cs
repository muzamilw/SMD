using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class AdCampaignClickRateHistory
    {
        public long ClickRateId { get; set; }
        public Nullable<long> CampaignID { get; set; }
        public Nullable<double> ClickRate { get; set; }
        public Nullable<System.DateTime> RateChangeDateTime { get; set; }

        public virtual AdCampaign AdCampaign { get; set; }
    }
}
