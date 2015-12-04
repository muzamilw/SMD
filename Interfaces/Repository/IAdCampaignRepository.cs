using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IAdCampaignRepository : IBaseRepository<AdCampaign, long>
    {
        List<AdvertGridRequest> GetAdvertsByUserId();
    }
}
