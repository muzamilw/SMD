using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public PayOutResponseModelForApproval Get([FromUri]GetPagedListRequest request)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.PayOutHistory, PayOutHistory>());
            var obj = (dynamic)null;
            if (request.isFlage)
                obj = _payOutHistoryService.GetPayOutHistoryForApprovalStage1(request);
            else
               obj = _payOutHistoryService.GetPayOutHistoryForApprovalStage2(request);
            var retobj = new PayOutResponseModelForApproval();
            foreach (var item in obj.PayOutHistory)
            {
                retobj.PayOutHistory.Add(Mapper.Map<SMD.Models.DomainModels.PayOutHistory, PayOutHistory>(item));

            }
            retobj.TotalCount = obj.TotalCount;
            return retobj;
        }
        #endregion
    }
}
