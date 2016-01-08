using System;
using System.Collections.Generic;

namespace SMD.Models.DomainModels
{
    public class Transaction
    {
        public long TxId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? Type { get; set; }
        public int? DebitCredit { get; set; }
        public double? CreditAmount { get; set; }
        public double? DebitAmount { get; set; }
        public long? AccountId { get; set; }
        public double? TaxPerc { get; set; }
        public double? TaxValue { get; set; }
        public long? AdCampaignId { get; set; }
        public long? SQId { get; set; }
        public bool? isProcessed { get; set; }
        public int? CurrencyId { get; set; }
        public int? CurrencyRateId { get; set; }
        public int? Sequence { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<TransactionLog> TransactionLogs { get; set; }
    }
}
