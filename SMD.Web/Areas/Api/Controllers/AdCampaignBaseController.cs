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
            else if (request.RequestId == 12) //  get user quiz question data 
            {
                return new AdCampaignBaseResponse
                {
                    AdCampaigns = _campaignService.getQuizCampaigns().AdCampaigns.Select(sur => sur.CreateFromDropdown()),

                };
            }

            else
                if (request.RequestId == 6) //  get survey question data 
            {
                return new AdCampaignBaseResponse
                {
                    SurveyQuestions = _campaignService.GetALLSurveyQuestionData().SurveyQuestions.Select(ques => ques.CreateFromDropdowndd()),

                };
            }
                else
                    if (request.RequestId == 7) //  get survey question Answers 
                    {
                        return new AdCampaignBaseResponse
                        {
                            SurveyQuestions = _campaignService.GetSurveyQuestionAnser((int)request.QuestionId).SurveyQuestions.Select(ques => ques.CreateFromDropdowndd()),

                        };
                    }
            else if (request.RequestId == 13) // get branch addresses 
            {

                return new AdCampaignBaseResponse
                {
                    listBranches = _campaignService.getCompanyBranches().listBranches.Select(sur => sur.CreateFromDropdown()),

                };
            }
            else //  get base data 
            {
                return _campaignService.GetCampaignBaseData().CreateCampaignBaseResponseFrom();

               
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
            else if (argsList[1] == "2") /// get languages list
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
            else if (argsList[1] == "3")  // get industry
            {
                IEnumerable<SMD.MIS.Areas.Api.Models.Industry> listOfIndustry = _campaignService.SearchIndustry(argsList[0]).Select(lang => lang.CreateFrom());

                if (listOfIndustry != null && listOfIndustry.Count() > 10)
                {
                    listOfIndustry = listOfIndustry.Take(10);
                }
                return new AdCampaignBaseResponse
                {
                    listIndustry = listOfIndustry
                };
            }
            else  // get education  4
            {
                IEnumerable<SMD.MIS.Areas.Api.Models.Education> listOfEducation = _campaignService.SearchEducation(argsList[0]).Select(edu => edu.CreateFrom());

                if (listOfEducation != null && listOfEducation.Count() > 10)
                {
                    listOfEducation = listOfEducation.Take(10);
                }
                return new AdCampaignBaseResponse
                {
                    listEducation = listOfEducation
                };
            }
            
        }
        #endregion
    }
}
