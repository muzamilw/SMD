using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IPayOutHistoryService
    {
        PayOutResponseModelForApproval GetPayOutHistoryForApprovalStage1(GetPagedListRequest request);
        PayOutResponseModelForApproval GetPayOutHistoryForApprovalStage2(GetPagedListRequest request);
    }
}
