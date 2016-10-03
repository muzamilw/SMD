using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class getCampaignsByStatus_Result
    {
        public string rectype { get; set; }
        public Nullable<int> Draft { get; set; }
        public Nullable<int> Pending { get; set; }
        public Nullable<int> Live { get; set; }
        public Nullable<int> Paused { get; set; }
        public Nullable<int> Rejected { get; set; }
    }
}
