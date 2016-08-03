using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class FavouriteCouponResponse : BaseApiResponse
    {
        public IEnumerable<Coupons> FavouriteCoupon { get; set; }
    }


    public class UserPurchasedCouponResponse : BaseApiResponse
    {
        public IEnumerable<Coupons> PurchasedCoupon { get; set; }
    }
}