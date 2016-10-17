using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IPayOutHistoryRepository : IBaseRepository<PayOutHistory, long>
    {

        List<PayOutHistory> GetPendingStageOnePayOuts();

        List<PayOutHistory> GetPendingStageTwoPayOuts();

        IEnumerable<PayOutHistory> GetPayOutHistoryForApprovalStage1(GetPagedListRequest request, out int rowCount);
        IEnumerable<PayOutHistory> GetPayOutHistoryForApprovalStage2(GetPagedListRequest request, out int rowCount);



    }


}
