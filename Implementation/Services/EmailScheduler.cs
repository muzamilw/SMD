using FluentScheduler;
using Microsoft.Practices.Unity;
using SMD.Implementation.Identity;
using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class EmailScheduler
    {
        [Dependency]
        private static IEmailManagerService EmailManagerService { get; set; }
        public static void UserTrainingEmailAfterThreeDays(Registry registry)
        {
            
            // Registration of Debit Process Scheduler Run after every 7 days 
          registry.Schedule(UserTrainingEmail).ToRunEvery(1).Days();
        }

        public static void MonitorQueue(Registry registry)
        {

            // Registration of Debit Process Scheduler Run after every 7 days 
            registry.Schedule(SendEmailFromQueue).ToRunNow().AndEvery(5).Minutes();
        }
        public static void UserTrainingEmail()
        {
           
        }
        public static void SendEmailFromQueue()
        {

        }
    }
}
