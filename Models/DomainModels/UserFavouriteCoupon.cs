using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class UserFavouriteCoupon
    {
        public long FavouriteCouponId { get; set; }
        public Nullable<long> CouponId { get; set; }
        public string UserId { get; set; }
    }
}
