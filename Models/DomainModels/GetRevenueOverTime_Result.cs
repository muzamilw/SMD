using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetRevenueOverTime_Result
    {
        public Nullable<double> amountcollected { get; set; }

        public string granular { get; set; }
    }
}
