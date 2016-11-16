using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{

    
    public partial class UserCouponView
    {
        public long UserCouponViewId { get; set; }
        public Nullable<long> CouponId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> ViewDateTime { get; set; }
        public string userLocationLAT { get; set; }
        public string userLocationLONG { get; set; }
    }
}
