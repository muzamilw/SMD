using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetPayOutHistoryController : ApiController
    {
        #region Attributes
        private readonly IPayOutHistoryService _payOutHistoryService;

        #endregion

        #region Constructor
        public GetPayOutHistoryController(IPayOutHistoryService _payOutHistoryService)
        {
            this._payOutHistoryService = _payOutHistoryService;
        
        }
        #endregion
        #region Methods
        public List<PayOutHistory> Get(int companyID)
        {
            var obj= _payOutHistoryService.GetPayOutHistoryByCompanyId(companyID);
            return obj;
        }

        #endregion
    }
}
