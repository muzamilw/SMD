using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getUserActivitiesOverTime_Result
    {
        public string Granual { get; set; }
        public Int64 newStats { get; set; }
        public Int64 loginStats { get; set; }
        public Int64 deleteStats { get; set; }
    }
}
