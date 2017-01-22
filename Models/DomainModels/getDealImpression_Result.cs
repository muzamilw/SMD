using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getDealImpressionByAgeByCouponId_Result
    {
        public Nullable<int> Stats { get; set; }
        public string label { get; set; }
    }
    public partial class getDealImpressionByGenderByCouponId_Result
    {
        public Nullable<int> Stats { get; set; }
        public string label { get; set; }
    }
    public partial class getDealImpressionByProfessionByCouponId_Result
    {
        public Nullable<int> Stats { get; set; }
        public string label { get; set; }
    }
}
