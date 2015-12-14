using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMD.Implementation.Services
{
    public class AdvertService : IAdvertService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IAdCampaignRepository _adCampaignRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAdCampaignTargetLocationRepository _adCampaignTargetLocationRepository;
        private readonly IAdCampaignTargetCriteriaRepository _adCampaignTargetCriteriaRepository;
        private readonly IEmailManagerService emailManagerService;
        private readonly IProfileQuestionRepository _profileQuestionRepository;
        private readonly IProfileQuestionAnswerRepository _profileQuestionAnswerRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public AdvertService(
            IAdCampaignRepository adCampaignRepository, 
            ILanguageRepository languageRepository,
            ICountryRepository countryRepository, 
            ICityRepository cityRepository,
            IAdCampaignTargetLocationRepository adCampaignTargetLocationRepository,
             IEmailManagerService emailManagerService,
            IAdCampaignTargetCriteriaRepository adCampaignTargetCriteriaRepository,
            IProfileQuestionRepository profileQuestionRepository,
            IProfileQuestionAnswerRepository profileQuestionAnswerRepository,
            ISurveyQuestionRepository surveyQuestionRepository)
        {
            this._adCampaignRepository = adCampaignRepository;
            this._languageRepository = languageRepository;
            this._countryRepository = countryRepository;
            this._cityRepository = cityRepository;
            this._adCampaignTargetLocationRepository = adCampaignTargetLocationRepository;
            this._adCampaignTargetCriteriaRepository = adCampaignTargetCriteriaRepository;
            this.emailManagerService = emailManagerService;
            this._profileQuestionRepository = profileQuestionRepository;
            this._profileQuestionAnswerRepository = profileQuestionAnswerRepository;
            this._surveyQuestionRepository = surveyQuestionRepository;
        }
        public List<CampaignGridModel> GetCampaignByUserId()
        {
            return _adCampaignRepository.GetCampaignByUserId();
        }

        /// <summary>
        /// Get Base Data 
        /// </summary>
        public AdCampaignBaseResponse GetCampaignBaseData()
        {
            return new AdCampaignBaseResponse
            {
                Languages = _languageRepository.GetAllLanguages()
            };
        }

        /// <summary>
        /// Get cities and countries search date
        /// </summary>
        public AdCampaignBaseResponse SearchCountriesAndCities(string searchString)
        {
            return new AdCampaignBaseResponse
            {
                countries = _countryRepository.GetSearchedCountries(searchString),
                Cities = _cityRepository.GetSearchCities(searchString)
            };
        }

        /// <summary>
        /// Get langauge search data
        /// </summary>
        public AdCampaignBaseResponse SearchLanguages(string searchString)
        {
            return new AdCampaignBaseResponse
            {
                Languages = _languageRepository.GetSearchedLanguages(searchString)
            };
        }

        /// <summary>
        /// Add Campaign
        /// </summary>
        public bool CreateCampaign(AdCampaign campaignModel)
        {
            _adCampaignRepository.Add(campaignModel);
            _adCampaignRepository.SaveChanges();
            long campaignId = campaignModel.CampaignId;
            if (campaignId > 0)
            {
                 char[] separator = new char[] { '|' };
                 List<string> argsList = null;
                if(campaignModel.Languages != null)
                {
                    foreach (string item in campaignModel.Languages) 
                    {
                        AdCampaignTargetCriteria oTargetCriteria = new AdCampaignTargetCriteria();
                        oTargetCriteria.CampaignId = campaignId;
                        oTargetCriteria.LanguageId = Convert.ToInt32(item);
                        oTargetCriteria.Type = (int)AdCampaignCriteriaType.Language;
                        _adCampaignTargetCriteriaRepository.Add(oTargetCriteria);
                    }
                    _adCampaignTargetCriteriaRepository.SaveChanges();
                }

                if (campaignModel.Cities != null)
                {
                    foreach (string item in campaignModel.Cities)
                    {
                        argsList = item.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                        AdCampaignTargetLocation oTargetLocation = new AdCampaignTargetLocation();

                        oTargetLocation.CampaignId = campaignId;
                        oTargetLocation.CityId = Convert.ToInt32(argsList[1]);
                        oTargetLocation.IncludeorExclude = Convert.ToBoolean(argsList[0]);
                        _adCampaignTargetLocationRepository.Add(oTargetLocation);
                    }
                    _adCampaignTargetLocationRepository.SaveChanges();
                }

                if (campaignModel.Countries != null)
                {
                    foreach (string item in campaignModel.Countries)
                    {
                        argsList = item.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                        AdCampaignTargetLocation oTargetLocation = new AdCampaignTargetLocation();

                        oTargetLocation.CampaignId = campaignId;
                        oTargetLocation.CityId = Convert.ToInt32(argsList[1]);
                        oTargetLocation.IncludeorExclude = Convert.ToBoolean(argsList[0]);
                        _adCampaignTargetLocationRepository.Add(oTargetLocation);
                    }
                    _adCampaignTargetLocationRepository.SaveChanges();
                }
                return true;
            }
            else 
            {
                return false;
            }
           
        }
        public CampaignResponseModel GetCampaigns(AdCampaignSearchRequest request)
        {
            int rowCount;
            return new CampaignResponseModel
            {
                Campaign = _adCampaignRepository.SearchCampaign(request, out rowCount),
                Languages = _languageRepository.GetAllLanguages(),
                TotalCount = rowCount
            };
        }
        #endregion
        #region Public

        /// <summary>
        /// Get Ad Campaigns that are need aprroval | baqer
        /// </summary>
        public AdCampaignResposneModelForAproval GetAdCampaignForAproval(AdCampaignSearchRequest request)
        {
            int rowCount;
            return new AdCampaignResposneModelForAproval
            {
                AdCampaigns = _adCampaignRepository.SearchAdCampaigns(request, out rowCount),
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// Update Ad CAmpaign | baqer
        /// </summary>
        public AdCampaign UpdateAdCampaign(AdCampaign source)
        {
            var dbAd =_adCampaignRepository.Find(source.CampaignId);
            // Update 
            if (dbAd != null)
            {
                // Approval
                if (source.Approved == true)
                {
                    dbAd.Approved = true;
                    dbAd.ApprovalDateTime = DateTime.Now;
                    dbAd.ApprovedBy = _adCampaignRepository.LoggedInUserIdentity;
                    dbAd.Status = (Int32) AdCampaignStatus.Live;
                    emailManagerService.SendQuestionApprovalEmail(dbAd.UserId);
                }
                // Rejection 
                else
                {
                    dbAd.Status = (Int32)AdCampaignStatus.ApprovalRejected;
                    dbAd.Approved = false;
                    dbAd.RejectedReason = source.RejectedReason;
                    emailManagerService.SendQuestionRejectionEmail(dbAd.UserId);
                }
                dbAd.ModifiedDateTime = DateTime.Now;
                dbAd.ModifiedBy = _adCampaignRepository.LoggedInUserIdentity;

                _adCampaignRepository.SaveChanges();
                return _adCampaignRepository.Find(source.CampaignId);
            }
            return new AdCampaign();
        }

            /// <summary>
        /// Get profile questions 
        /// </summary>
        public AdCampaignBaseResponse GetProfileQuestionData()
        {
            return new AdCampaignBaseResponse
            {
                ProfileQuestions = _profileQuestionRepository.GetAll()
            };
        }

        /// <summary>
        /// Get profile answers by question id 
        /// </summary>
        public AdCampaignBaseResponse GetProfileQuestionAnswersData(int QuestionId)
        {
            return new AdCampaignBaseResponse
            {
                ProfileQuestionAnswers =
                    _profileQuestionAnswerRepository.GetAllProfileQuestionAnswerByQuestionId(QuestionId)
            };
        }
        /// <summary>
        /// Get survey questions 
        /// </summary>
        public AdCampaignBaseResponse GetSurveyQuestionData()
        {
            return new AdCampaignBaseResponse
            {
                SurveyQuestions = _surveyQuestionRepository.GetAll()
            };
        }

        #endregion
    }
}
