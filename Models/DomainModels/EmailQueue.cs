using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Models.DomainModels
{
    public class EmailQueue
    {
        public int EmailQueueId { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string EmailFrom { get; set; }
        public Nullable<short> Type { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Images { get; set; }
        public Nullable<System.DateTime> SendDateTime { get; set; }
        public Nullable<byte> IsDeliverd { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPServer { get; set; }
        public string ErrorResponse { get; set; }
        public string FileAttachment { get; set; }
        public Nullable<int> AttemptCount { get; set; }
        public string ToName { get; set; }
        public string FromName { get; set; }
    }
}
