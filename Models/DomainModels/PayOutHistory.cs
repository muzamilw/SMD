using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class PayOutHistory
    {
        public long PayOutId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<System.DateTime> RequestDateTime { get; set; }
        public Nullable<double> CentzAmount { get; set; }
        public Nullable<double> DollarAmount { get; set; }
        public Nullable<bool> StageOneStatus { get; set; }
        public string StageOneRejectionReason { get; set; }
        public Nullable<System.DateTime> StageOneEventDate { get; set; }
        public string StageOneUserId { get; set; }
        public Nullable<bool> StageTwoStatus { get; set; }
        public string StageTwoRejectionReason { get; set; }
        public Nullable<System.DateTime> StageTwoEventDate { get; set; }
        public string StageTwoUserId { get; set; }
        public string TargetPayoutAccount { get; set; }

        public virtual Company Company { get; set; }
    }
}
