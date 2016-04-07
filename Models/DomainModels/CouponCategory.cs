using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class CouponCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Status { get; set; }
        public virtual ICollection<AdCampaign> AdCampaigns { get; set; }
        public virtual ICollection<CampaignCategory> CampaignCategories { get; set; }

    }
}
