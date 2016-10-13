using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getDealByCouponID_Result
    {
        public string Granual { get; set; }
        public Nullable<long> openStats { get; set; }
        public Nullable<long> ConvrsionStats { get; set; }
    }
}
