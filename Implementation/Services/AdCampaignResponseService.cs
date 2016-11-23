using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class AdCampaignResponseService : IAdCampaignResponseService
    {

        #region private
        private readonly IAdCampaignResponseRepository _adCampaignResponseRepository;
        #endregion
        public AdCampaignResponseService(IAdCampaignResponseRepository adCampaignResponseRepository)
        {
            _adCampaignResponseRepository = adCampaignResponseRepository;
        }

        #region public
        public int getCampaignByIdQQFormAnalytic(long CampaignId, int Choice, int Gender, int age, string Profession, string City, int type, int questionId)
        {
           return _adCampaignResponseRepository.getCampaignByIdQQFormAnalytic(CampaignId,  Choice, Gender, age, Profession, City, type, questionId);
        }

        #endregion
    }
}
