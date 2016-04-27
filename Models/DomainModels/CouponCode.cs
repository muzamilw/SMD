using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class CouponCode
    {
        public long CodeId { get; set; }
        public Nullable<long> CampaignId { get; set; }
        public string Code { get; set; }
        public Nullable<bool> IsTaken { get; set; }
        public string UserId { get; set; }
        public DateTime? TakenDateTime { get; set; }
        public virtual AdCampaign AdCampaign { get; set; }
        public virtual User AspNetUser { get; set; }
    }
}
