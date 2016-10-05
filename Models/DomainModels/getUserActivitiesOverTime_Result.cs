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
        public int newStats { get; set; }
        public int loginStats { get; set; }
        public int deleteStats { get; set; }
    }
}
