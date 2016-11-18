using AutoMapper;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetCountryController : ApiController
    {
        #region Attributes

        private readonly ICompanyBranchService _companyBranchservice;
        #endregion

        #region Constructor
        public GetCountryController(ICompanyBranchService _companyBranchservice)
        {
            this._companyBranchservice = _companyBranchservice;
        }

        #endregion


        #region Method
        public List<CountryDropdown> Get()
        {

            var result = new List<CountryDropdown>();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Country>, List<CountryDropdown>>());
            return Mapper.Map<List<Country>, List < CountryDropdown >>( _companyBranchservice.GetAllCountries());

            //var obj = _companyBranchservice.GetAllCountries();
            //var retobj = new List<CountryDropdown>();
            //foreach (var item in obj)
            //{
            //    CountryDropdown objCountry = new CountryDropdown();
            //    objCountry.CountryId = item.CountryId;
            //    objCountry.CountryName =item.CountryName;
            //    retobj.Add(objCountry);
               
            //}
            //return retobj;
        }
        #endregion
    }
}
