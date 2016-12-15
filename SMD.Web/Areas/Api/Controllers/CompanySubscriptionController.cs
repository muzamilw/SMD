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
    public class CompanySubscriptionController : ApiController
    {
        #region Attribues
        private readonly ICompanyService companyService;
        #endregion 

        #region Constructors
        public CompanySubscriptionController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Methods
        public CompanySubscription Get()
        {
            var res = companyService.GetCompanySubscription();
            return res;
        }
        public string Post(CompanySubscription comSub)
        {
            if (comSub == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyService.UpdateCompanySubscription(comSub);
        }
        #endregion
    }
}
