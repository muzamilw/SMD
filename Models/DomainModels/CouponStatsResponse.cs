using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
   public class CouponStatsResponse
    {
       public long DealLines { get; set; }
       public long ClickThruCount { get; set; }
       public double ClickThruComparison { get; set; }
       public long ClickThruDirection { get; set; }
       public double DealsOpenedComparison { get; set; }
       public long DealsOpenedDirection { get; set; }
       public double DealRating { get; set; }
       public long DealReviewsCount { get; set; }
       public long CouponId { get; set; }
    }
}
