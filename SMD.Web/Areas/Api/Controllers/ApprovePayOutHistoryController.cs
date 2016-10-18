using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ApprovePayOutHistoryController : ApiController
    {
        #region Attributes
        private readonly IPayOutHistoryService _payOutHistoryService;
        #endregion 

        #region Constructors
        public ApprovePayOutHistoryController(IPayOutHistoryService _payOutHistoryService)
        {
            this._payOutHistoryService = _payOutHistoryService;
        
        }
        #endregion

        #region Methods
        public SMD.Models.ResponseModels.PayOutResponseModelForApproval Get([FromUri]GetPagedListRequest request)
        {
            var obj = request.UserRole == "Franchise_Approver1" ? _payOutHistoryService.GetPayOutHistoryForApprovalStage1(request) : _payOutHistoryService.GetPayOutHistoryForApprovalStage2(request);
            return obj;
        }
        public string Post(PayOutHistory payOut)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PayOutHistory, SMD.Models.DomainModels.PayOutHistory>());
            if (payOut == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _payOutHistoryService.UpdatePayOutForApproval(Mapper.Map<PayOutHistory, SMD.Models.DomainModels.PayOutHistory>(payOut));
        }
        #endregion
    }
}
