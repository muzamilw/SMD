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
    public class GetCitiesController : ApiController
    {
        #region Attrubites
        private readonly ICompanyBranchService _companyBranchservice;
        #endregion

        #region Constructor
        public GetCitiesController(ICompanyBranchService _companyBranchservice)
        {
            this._companyBranchservice = _companyBranchservice;
        }

        #endregion

        #region  Method
        public List<CityDropDown> Get()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SMD.Models.DomainModels.City, CountryDropdown>());
            var obj = _companyBranchservice.GetAllCities();
            var retobj = new List<CityDropDown>();
            foreach (var item in obj)
            {
                CityDropDown objCity = new CityDropDown();
                objCity.CityId = item.CityId;
                objCity.CityName = item.CityName;
                objCity.CountryId = item.CountryId;
                retobj.Add(objCity);

            }
            return retobj;
        }

        #endregion
    }
}
