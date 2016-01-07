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
             CollectionScheduler.SetDebitScheduler(this);

            // Credit Scheduler
             PayOutScheduler.SetDebitScheduler(this);

            // Ad more Schedulers here ...
        }
    }

}
