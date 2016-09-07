using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public partial class GetTransactions_Result
    {
        public long TxID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> DebitCredit { get; set; }
        public Nullable<double> CreditAmount { get; set; }
        public Nullable<double> DebitAmount { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<double> TaxPerc { get; set; }
        public Nullable<double> TaxValue { get; set; }
        public Nullable<long> AdCampaignID { get; set; }
        public Nullable<long> SQID { get; set; }
        public Nullable<bool> isProcessed { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> CurrencyRateID { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<long> CouponId { get; set; }
        public Nullable<double> AccountBalance { get; set; }
        public Nullable<int> PQID { get; set; }
        public Nullable<double> CurrentBalance { get; set; }
        public string description { get; set; }
    }
}
