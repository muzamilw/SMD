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
    
    public partial class Log
    {
        public Log()
        {
            this.CategoryLogs = new HashSet<CategoryLog>();
        }
    
        public int LogID { get; set; }
        public Nullable<int> EventID { get; set; }
        public int Priority { get; set; }
        public string Severity { get; set; }
        public string Title { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string MachineName { get; set; }
        public string AppDomainName { get; set; }
        public string ProcessID { get; set; }
        public string ProcessName { get; set; }
        public string ThreadName { get; set; }
        public string Win32ThreadId { get; set; }
        public string Message { get; set; }
        public string FormattedMessage { get; set; }
    
        public virtual ICollection<CategoryLog> CategoryLogs { get; set; }
    }
}
