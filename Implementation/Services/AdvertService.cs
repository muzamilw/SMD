using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IAdCampaignTargetCriteriaRepository adCampaignTargetCriteriaRepository)
        {
            this._adCampaignRepository = adCampaignRepository;
            this._languageRepository = languageRepository;
            this._countryRepository = countryRepository;
            this._cityRepository = cityRepository;
            this._adCampaignTargetLocationRepository = adCampaignTargetLocationRepository;
            this._adCampaignTargetCriteriaRepository = adCampaignTargetCriteriaRepository;
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
        public bool AddCampaign(AdCampaign campaignModel)
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
        #endregion
    }
}
