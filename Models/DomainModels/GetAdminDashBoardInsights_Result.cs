using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetAdminDashBoardInsights_Result
    {
        public string rectype { get; set; }
        public string pMonth { get; set; }
        public Nullable<double> us { get; set; }
        public Nullable<double> uk { get; set; }
        public Nullable<double> ca { get; set; }
        public Nullable<double> au { get; set; }
        public Nullable<double> ae { get; set; }
    }
}
