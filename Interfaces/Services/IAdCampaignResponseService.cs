using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IAdCampaignResponseService
    {
        int getCampaignByIdQQFormAnalytic(long CampaignId, int Choice, int Gender, int age, string Profession, string City, int type, int questionId);
        List<CampaignResponseLocation> getCampaignUserLocationByCId(long campaignId);
    }
}
