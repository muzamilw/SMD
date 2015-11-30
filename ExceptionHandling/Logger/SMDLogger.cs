using System.Collections.Generic;
using System.Diagnostics;
using SMD.Interfaces.Logger;

namespace SMD.ExceptionHandling.Logger
{
    public sealed class SMDLogger : ISMDLogger
    {
        public void Write(object message, string category, int priority, int eventId, TraceEventType severity)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, severity);
        }

        public void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, severity, title);
        }

        public void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title,
            IDictionary<string, object> properties)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, severity, title, properties);
        }
    }
}
