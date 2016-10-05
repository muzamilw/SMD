using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetRevenueByCampaignOverTime_Result
    {
        public string Granual { get; set; }
        public decimal Revenue { get; set; }
    }
}
