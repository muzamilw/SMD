using System;

namespace SMD.Models.DomainModels
{
    /// <summary>
    /// Transaction Log keeper
    /// </summary>
    public class TransactionLog
    {
        /// <summary>
        /// Transaction Log Id PK
        /// </summary>
        public long TransactionLogId { get; set; }

        /// <summary>
        /// Transaction Id
        /// </summary>
        public long TxId { get; set; }

        /// <summary>
        /// Log Date Time
        /// </summary>
        public DateTime LogDate { get; set; }

        /// <summary>
        /// Amount of Transation made
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Type of Transation 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Transaction From User Id
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// Transaction To User Id
        /// </summary>
        public string ToUser { get; set; }

        /// <summary>
        /// State of Transaction
        /// </summary>
        public Boolean IsCompleted { get; set; }


        public virtual Transaction Transaction { get; set; }
    }
}
