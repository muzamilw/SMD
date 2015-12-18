using SMD.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Account
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public Nullable<int> AccountType { get; set; }
        public Nullable<decimal> AccountBalance { get; set; }
        public string UserId { get; set; }

        public virtual User AspNetUser { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
