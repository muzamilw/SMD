using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.MIS.Areas.Api.Models;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class AdCampaignBaseController : ApiController
    {
        #region Public
        private readonly IAdvertService _campaignService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public AdCampaignBaseController(IAdvertService campaignService)
        {
            this._campaignService = campaignService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get base data for campaigns 
        /// 
        /// </summary>
        public AdCampaignBaseResponse getBaseData()
        {
            IEnumerable<LocationDropDown> listOfcount = _campaignService.GetCampaignBaseData().countries.Select(coun => coun.CreateCountryLocationFrom());
            IEnumerable<LocationDropDown> listOfcity = _campaignService.GetCampaignBaseData().Cities.Select(coun => coun.CreateCityLocationFrom());
            IEnumerable<LocationDropDown> listOfAllCC = listOfcity;
            listOfAllCC = listOfAllCC.Concat(listOfcount);
            return new AdCampaignBaseResponse
            {
               // Languages = _campaignService.GetCampaignBaseData().Languages.Select(lang => lang.CreateFrom()),
                countriesAndCities = listOfAllCC
            };
        }
        public AdCampaignBaseResponse Post(string searchText)
        {
            char[] separator = new char[] { '|' };
            List<string> argsList = searchText.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (argsList[1] == "1")
            {
                IEnumerable<LocationDropDown> listOfcount = _campaignService.SearchCountriesAndCities(argsList[0]).countries.Select(coun => coun.CreateCountryLocationFrom());
                IEnumerable<LocationDropDown> listOfcity = _campaignService.SearchCountriesAndCities(argsList[0]).Cities.Select(coun => coun.CreateCityLocationFrom());
                IEnumerable<LocationDropDown> listOfAllCC = listOfcity;
                listOfAllCC = listOfAllCC.Concat(listOfcount);
                if (listOfAllCC != null && listOfAllCC.Count() > 10)
                {
                    listOfAllCC = listOfAllCC.Take(10);
                }
                return new AdCampaignBaseResponse
                {

                    countriesAndCities = listOfAllCC
                };
            }
            else 
            {
                IEnumerable<LanguageDropdown> listOfLangs = _campaignService.SearchLanguages(argsList[0]).Languages.Select(lang => lang.CreateFrom());
                if (listOfLangs != null && listOfLangs.Count() > 10)
                {
                    listOfLangs = listOfLangs.Take(10);
                }
                return new AdCampaignBaseResponse
                {
                    Languages = listOfLangs
                };
            }
            
        }
        #endregion
    }
}
