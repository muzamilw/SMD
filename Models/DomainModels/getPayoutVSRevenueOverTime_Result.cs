using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getPayoutVSRevenueOverTime_Result
    {
        public string Granual { get; set; }

        public Nullable<double> revStats { get; set; }

        public Nullable<double> PayoutStats { get; set; }

    }
}
