using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetCompanyController : ApiController
    {
        #region Attributes 
        private readonly ICompanyService _companyService;
        #endregion

        #region Constructor
        public GetCompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        
        }
        #endregion

        #region methods
        public CompanybillingAddress Get()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.Company, CompanybillingAddress>());
            var obj = _companyService.GetCompanyForAddress();
            CompanybillingAddress objaddress = new CompanybillingAddress();
            objaddress.BillingAddressLine1 = obj.BillingAddressLine1;
            objaddress.BillingAddressLine2 = obj.BillingAddressLine2;
            //objaddress.BillingCityId = obj.BillingCityId;
            objaddress.BillingCity = obj.BillingCity;
            objaddress.BillingCountryId = obj.BillingCountryId;
            objaddress.BillingState = obj.BillingState;
            objaddress.BillingZipCode = obj.BillingZipCode;
            objaddress.BillingPhone = obj.BillingPhone;
            return objaddress;
            //return Mapper.Map<SMD.Models.DomainModels.Company, CompanybillingAddress>(obj);
        }
         
        #endregion
    }
}
