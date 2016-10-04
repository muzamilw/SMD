using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetLiveCampaignCountOverTime_Result
    {
        public string Granual { get; set; }
        public int campCount { get; set; }
    }
}
