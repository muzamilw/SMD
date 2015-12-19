using SMD.Models.IdentityModels;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class Account
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public int? AccountType { get; set; }
        public decimal? AccountBalance { get; set; }
        public string UserId { get; set; }

        public virtual User AspNetUser { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
