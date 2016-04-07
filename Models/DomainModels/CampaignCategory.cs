using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class CampaignCategory
    {
        public long CampaignId { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }

        public virtual AdCampaign AdCampaign { get; set; }
        public virtual CouponCategory CouponCategory { get; set; }
    }
}
