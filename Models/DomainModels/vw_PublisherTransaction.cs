using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class vw_PublisherTransaction 
    {
        public long tid { get; set; }
        public Nullable<System.DateTime> TDate { get; set; }
        public Nullable<int> Deposit { get; set; }
        public Nullable<double> Withdrawal { get; set; }
        public string Transaction { get; set; }
        public string userId { get; set; }
        public Nullable<int> ownerCompanyId { get; set; }
        public Nullable<double> CurentBalance { get; set; }
        public long CampaignID { get; set; }
        public string Email { get; set; }
    }
}
