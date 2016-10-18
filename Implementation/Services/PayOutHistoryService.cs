using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
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
        private readonly ICompanyRepository _CompanyRepository;
        private readonly IAspnetUsersRepository _IAspnetUsersRepository;
        public PayOutHistoryService(IPayOutHistoryRepository _PayOutHistoryRepository, ICompanyRepository _CompanyRepository, IAspnetUsersRepository _IAspnetUsersRepository)
        {
            this._PayOutHistoryRepository = _PayOutHistoryRepository;
            this._CompanyRepository = _CompanyRepository;
            this._IAspnetUsersRepository = _IAspnetUsersRepository;
        }
        public PayOutResponseModelForApproval GetPayOutHistoryForApprovalStage1(GetPagedListRequest request)
        {
            int rowCount;
            var history = _PayOutHistoryRepository.GetPayOutHistoryForApprovalStage1(request, out rowCount).ToList();
            history.ForEach(a => a.CompanyName = _CompanyRepository.GetCompanyNameByID(a.CompanyId ?? 0));
            history.ForEach(a => a.UserId = _IAspnetUsersRepository.GetUserid(a.CompanyId ?? 0));
            history.ForEach(a => a.Email = _IAspnetUsersRepository.GetUserEmail(a.CompanyId ?? 0));
            history.ForEach(a => a.Company = null);
            return new PayOutResponseModelForApproval
            {
                PayOutHistory = history,
                TotalCount = rowCount
            };
        }
        public PayOutResponseModelForApproval GetPayOutHistoryForApprovalStage2(GetPagedListRequest request)
        {
            int rowCount;
            var history = _PayOutHistoryRepository.GetPayOutHistoryForApprovalStage2(request, out rowCount).ToList();
            history.ForEach(a => a.CompanyName = _CompanyRepository.GetCompanyNameByID(a.CompanyId ?? 0));
            history.ForEach(a => a.UserId = _IAspnetUsersRepository.GetUserid(a.CompanyId ?? 0));
            history.ForEach(a => a.Email = _IAspnetUsersRepository.GetUserEmail(a.CompanyId ?? 0));
            history.ForEach(a => a.Company = null);
            return new PayOutResponseModelForApproval
            {
                PayOutHistory = history,
                TotalCount = rowCount
            };
        }
        public string UpdatePayOutForApproval(PayOutHistory source)
        {
            string respMesg = "True";
            var dbCo = _PayOutHistoryRepository.Find(source.PayOutId);
            String userid = _PayOutHistoryRepository.LoggedInUserIdentity;
            if (dbCo != null)
            {
                if (dbCo.StageOneStatus == null)
                {
                    dbCo.StageOneStatus = source.StageOneStatus;
                    dbCo.StageOneUserId = userid;
                    dbCo.StageOneRejectionReason = source.StageOneRejectionReason.ToString();
                }
                else
                {
                    dbCo.StageTwoStatus = source.StageTwoStatus;
                    dbCo.StageTwoUserId = userid;
                    dbCo.StageTwoRejectionReason = source.StageTwoRejectionReason.ToString();

                }
                _PayOutHistoryRepository.SaveChanges();

            }
            return respMesg;
        }

        public List<PayOutHistory> GetPayOutHistoryByCompanyId(int companyId)
        {
            var historyList = _PayOutHistoryRepository.GetPayOutHistoryByCompanyId(companyId);
            historyList.ForEach(a => a.CompanyName = _CompanyRepository.GetCompanyNameByID(companyId));
            historyList.ForEach(a => a.UserId = _IAspnetUsersRepository.GetUserid(companyId));
            historyList.ForEach(a => a.Email = _IAspnetUsersRepository.GetUserEmail(companyId));
            historyList.ForEach(a => a.Company = null);
            return historyList;
        }


    }
}
