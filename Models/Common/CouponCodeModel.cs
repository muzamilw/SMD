using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.Common
{
    public class CouponCodeModel
    {
        public int CouponQuantity { get; set; }
        public List<CouponCode> CouponList { get; set; }
    }
}
