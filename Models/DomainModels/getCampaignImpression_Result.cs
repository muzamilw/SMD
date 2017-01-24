using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getCampaignImpressionByGenderByCId_Result
    {
        public Nullable<int> Stats { get; set; }
        public string label { get; set; }
    }
    public partial class getCampaignImpressionByProfessionByCId_Result
    {
        public Nullable<int> Stats { get; set; }
        public string label { get; set; }
    }
    public partial class getCampaignImpressionByAgeByCId_Result
    {
        public Nullable<int> Stats { get; set; }
        public string label { get; set; }
    }
}
