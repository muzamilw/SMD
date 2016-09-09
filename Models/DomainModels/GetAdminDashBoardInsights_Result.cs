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
        public Nullable<int> us { get; set; }
        public Nullable<int> uk { get; set; }
        public Nullable<int> ca { get; set; }
        public Nullable<int> au { get; set; }
        public Nullable<int> ae { get; set; }
    }
}
