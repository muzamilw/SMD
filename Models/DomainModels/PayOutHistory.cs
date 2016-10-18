using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.DomainModels
{
    public partial class PayOutHistory
    {
        public long PayOutId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<System.DateTime> RequestDateTime { get; set; }
        public Nullable<double> CentzAmount { get; set; }
        public Nullable<double> DollarAmount { get; set; }
        public Nullable<int> StageOneStatus { get; set; }
        public string StageOneRejectionReason { get; set; }
        public Nullable<System.DateTime> StageOneEventDate { get; set; }
        public string StageOneUserId { get; set; }
        public Nullable<int> StageTwoStatus { get; set; }
        public string StageTwoRejectionReason { get; set; }
        public Nullable<System.DateTime> StageTwoEventDate { get; set; }
        public string StageTwoUserId { get; set; }
        public string TargetPayoutAccount { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string UserId { get; set; }
        [NotMapped]
        public string Email { get; set; }

        public virtual Company Company { get; set; }
    }
}
