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
    public class CompanyInfoController : ApiController
    {
        private readonly ICompanyService companyService;

        public CompanyInfoController(ICompanyService companyService)
        {
           this.companyService=companyService;
        }

        public Company GetCompanyInfo()
        {
            return companyService.GetCompanyInfo();
        }

         
    }
}
