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
    public class GetApprovalCountController : ApiController
    {
        #region Attributes

        private readonly IWebApiUserService webApiUserService;
        #endregion

        #region Constructor
        public GetApprovalCountController(IWebApiUserService webApiUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
        }

        #endregion

        #region Method
        public GetApprovalCount_Result Get()
        {
            var response = webApiUserService.GetApprovalCount();
            return response;
        }


        #endregion
    }
}
