using FluentScheduler;
using System;
using System.Threading;
using SMD.Interfaces.Services;

namespace SMD.Implementation.Services
{
    public class Test : Registry
    {
        public Test()
        {
               
                // Schedule a simple task to run at a specific time
                Schedule(() => System.Diagnostics.Debug.Write("This is from service " + DateTime.Now.Second+"\n"))
                    .ToRunNow().AndEvery(2).Seconds();
        }
    }
}
