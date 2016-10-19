using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getAdsCampaignByCampaignIdtblAnalytic_Result
    {
        public int ordr { get; set; }
        public string label { get; set; }
        public Nullable<double> All_time { get; set; }
        public Nullable<double> C30_days { get; set; }
    }
    public partial class getPollBySQIDtblAnalytic_Result
    {
        public int ordr { get; set; }
        public string label { get; set; }
        public Nullable<double> All_time { get; set; }
        public Nullable<double> C30_days { get; set; }
    }
    public partial class getSurvayByPQIDtblAnalytic_Result
    {
        public int ordr { get; set; }
        public string label { get; set; }
        public Nullable<double> All_time { get; set; }
        public Nullable<double> C30_days { get; set; }
    }
}
