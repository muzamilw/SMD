using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class PayOutHistoryService : IPayOutHistoryService
    {
        private readonly IPayOutHistoryRepository _PayOutHistoryRepository;
        public PayOutHistoryService(IPayOutHistoryRepository _PayOutHistoryRepository)
        {
            this._PayOutHistoryRepository = _PayOutHistoryRepository;
        }
        public PayOutResponseModelForApproval GetPayOutHistoryForApprovalStage1(GetPagedListRequest request)
        {
            int rowCount;
            return new PayOutResponseModelForApproval
            {
                PayOutHistory = _PayOutHistoryRepository.GetPayOutHistoryForApprovalStage1(request, out rowCount).ToList(),
                TotalCount = rowCount
            };
        }
        public PayOutResponseModelForApproval GetPayOutHistoryForApprovalStage2(GetPagedListRequest request)
        {
            int rowCount;
            return new PayOutResponseModelForApproval
            {
                PayOutHistory = _PayOutHistoryRepository.GetPayOutHistoryForApprovalStage2(request, out rowCount).ToList(),
                TotalCount = rowCount
            };
        }
        
    }
}
