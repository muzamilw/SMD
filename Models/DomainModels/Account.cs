using SMD.Models.IdentityModels;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class Account
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public int? AccountType { get; set; }
        public double? AccountBalance { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
