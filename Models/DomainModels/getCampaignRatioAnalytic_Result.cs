using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getAdsCampaignByCampaignIdRatioAnalytic_Result
    {
        public string label { get; set; }
        public Nullable<int> value { get; set; }
    }
    public partial class getSurveyByPQIDRatioAnalytic_Result
    {
        public string label { get; set; }
        public Nullable<int> value { get; set; }
    }
    public partial class getPollBySQIDRatioAnalytic_Result
    {
        public int label { get; set; }
        public Nullable<int> value { get; set; }
    }
    public partial class getDealByCouponIdRatioAnalytic_Result
    {
        public string label { get; set; }
        public Nullable<int> value { get; set; }
    }
}
