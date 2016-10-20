using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getCampaignROItblAnalytic_Result
    {
        public int ordr { get; set; }
        public string label1 { get; set; }
        public Nullable<int> Stats1 { get; set; }
        public string label2 { get; set; }
        public Nullable<double> Stats2 { get; set; }
        public string label3 { get; set; }
        public Nullable<int> Stats3 { get; set; }
    }
}
