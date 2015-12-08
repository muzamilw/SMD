using SMD.Models.DomainModels;
using SMD.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IAdCampaignRepository : IBaseRepository<AdCampaign, long>
    {
        List<CampaignGridModel> GetCampaignByUserId();
    }
}
