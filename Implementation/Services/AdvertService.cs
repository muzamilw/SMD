using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
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
       

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public AdvertService(IAdCampaignRepository adCampaignRepository)
        {
            this._adCampaignRepository = adCampaignRepository;
            
        }
        public List<AdCampaign> GetAdverts()
        {
            return _adCampaignRepository.GetAdvertsByUserId();
        }
        #endregion
    }
}
