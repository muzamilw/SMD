using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models;
using SMD.Models.Common;
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
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public AdvertService(IAdCampaignRepository adCampaignRepository, ILanguageRepository languageRepository
            , ICountryRepository countryRepository)
        {
            this._adCampaignRepository = adCampaignRepository;
            this._languageRepository = languageRepository;
            this._countryRepository = countryRepository;
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
                Languages = _languageRepository.GetAllLanguages(),
               countries = _countryRepository.GetAllCountries()
            };
        }
        #endregion
    }
}
