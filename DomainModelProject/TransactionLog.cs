//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModelProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionLog
    {
        public long TransactionLogId { get; set; }
        public long TxId { get; set; }
        public System.DateTime LogDate { get; set; }
        public double Amount { get; set; }
        public int Type { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public string Description { get; set; }
    
        public virtual Transaction Transaction { get; set; }
    }
}
