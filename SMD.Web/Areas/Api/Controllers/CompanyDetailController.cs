using SMD.Interfaces.Services;
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
    public class CompanyDetailController : ApiController
    {
        #region Attributes 
        private readonly IWebApiUserService webApiUserService;
        private readonly ICompanyService companyService;
        #endregion

        #region Constructor
        public CompanyDetailController(IWebApiUserService webApiUserService, ICompanyService companyService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.companyService = companyService;
        }
        #endregion

        #region Methods
        public CompanyResponseModel Get(String userId,int companyId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            var response = companyService.GetCompanyDetails();
            return response;
        }

        #endregion
    }
}
