using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class CampaignEventHistory
    {
        public long EventId { get; set; }
        public Nullable<int> EventStatusId { get; set; }
        public Nullable<long> CampaignID { get; set; }
        public Nullable<System.DateTime> EventDateTime { get; set; }
        public Nullable<long> CouponId { get; set; }
        public Nullable<long> SQID { get; set; }
        public Nullable<int> PQID { get; set; }

        public virtual AdCampaign AdCampaign { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual EventStatus EventStatus { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
