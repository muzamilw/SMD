using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IPayOutHistoryRepository : IBaseRepository<PayOutHistory, long>
    {

        List<PayOutHistory> GetPendingStageOnePayOuts();

        List<PayOutHistory> GetPendingStageTwoPayOuts();
        
    }


}
