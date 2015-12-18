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
        public AdCampaignBaseResponse getBaseData([FromUri]CampaignSearchRequest request)
        {
            if (request.RequestId == 2) //  get profile question data 
            {
                return new AdCampaignBaseResponse
                {
                    ProfileQuestions = _campaignService.GetProfileQuestionData().ProfileQuestions.Select(ques => ques.CreateFromDropdown()),

                };
            }
            else if (request.RequestId == 3) //  get profile answer data 
            {
                return new AdCampaignBaseResponse
                {
                    ProfileQuestionAnswers = _campaignService.GetProfileQuestionAnswersData((int)request.QuestionId).ProfileQuestionAnswers.Select(ques => ques.CreateFromDropdown()),

                };
            }
            else if (request.RequestId == 4) //  get survey question data 
            {
                return new AdCampaignBaseResponse
                {
                    SurveyQuestions = _campaignService.GetSurveyQuestionData(request.SQID).SurveyQuestions.Select(sur => sur.CreateFromDropdown()),

                };
            }
            else //  get base data 
            {
                return new AdCampaignBaseResponse
                {
                    Languages = _campaignService.GetCampaignBaseData().Languages.Select(lang => lang.CreateFrom()),

                };
            }
           
        }
        public AdCampaignBaseResponse Post(string searchText)
        {
            char[] separator = new char[] { '|' };
            List<string> argsList = searchText.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (argsList[1] == "1") // get countries and cities list
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
            else // get languages list
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
