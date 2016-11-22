using System.Web;
using SMD.Interfaces.Services;
using System.Net;
using System.Web.Http;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.WebBase.Mvc;
using System.Collections.Generic;
using AutoMapper;
using SMD.Models.DomainModels;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class CountryController : ApiController
    {
          #region Public
        private readonly ICountryService countryService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        [ApiExceptionCustom]
        public List<CountryDropdown> Get(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            //var x = countryService.GetCountries()

            var result = new List<CountryDropdown>();
            Mapper.Initialize(cfg => cfg.CreateMap<Country, CountryDropdown>());
            var cc = countryService.GetCountries();
            var res =  Mapper.Map<List<Country>, List<CountryDropdown>>(cc);

            return res;
        }


        #endregion
    }
}
