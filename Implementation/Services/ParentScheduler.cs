using FluentScheduler;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Schedules's Register 
    /// </summary>
    public class ParentScheduler : Registry
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ParentScheduler()
        {
            // Debit Scheduler 
             DebitScheduler.SetDebitScheduler(this);

            // Credit Scheduler
             CreditScheduler.SetDebitScheduler(this);

            // Ad more Schedulers here ...
        }
    }

}
