using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ReferralComponiesController : ApiController
    {
        #region Attributes
        private readonly IWebApiUserService webApiUserService;
        private readonly ICompanyService companyService;
        #endregion

        #region Constructor
        public ReferralComponiesController(IWebApiUserService webApiUserService, ICompanyService companyService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
            this.companyService = companyService;
        }
        public List<vw_ReferringCompanies> Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            var response = companyService.GetRefferalComponiesByCid();
            return response;
        }

        #endregion

        #region Method


        #endregion
    }
}
