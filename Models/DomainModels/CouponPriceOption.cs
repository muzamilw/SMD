using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class CouponPriceOption
    {
        public long CouponPriceOptionId { get; set; }
        public Nullable<long> CouponId { get; set; }
        public string Description { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Savings { get; set; }
        public string VoucherCode { get; set; }
        public string OptionUrl { get; set; }


        public Nullable<DateTime> ExpiryDate { get; set; }
        public string URL { get; set; }

   
        

        public virtual Coupon Coupon { get; set; }
    }
}
