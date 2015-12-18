using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class Transaction
    {
        public long TxId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> DebitCredit { get; set; }
        public Nullable<double> CreditAmount { get; set; }
        public Nullable<double> DebitAmount { get; set; }
        public Nullable<long> AccountId { get; set; }
        public Nullable<double> TaxPerc { get; set; }
        public Nullable<double> TaxValue { get; set; }
        public Nullable<long> AdCampaignId { get; set; }
        public Nullable<long> SQId { get; set; }
        public Nullable<bool> isProcessed { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<int> CurrencyRateId { get; set; }
        public Nullable<int> Sequence { get; set; }

        public virtual Account Account { get; set; }
    }
}
