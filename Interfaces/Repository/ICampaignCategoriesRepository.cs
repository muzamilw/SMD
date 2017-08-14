using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface ICampaignCategoriesRepository: IBaseRepository<CampaignCategory, long>
    {
        void RemoveAll(List<CampaignCategory> categories);
        IEnumerable<getCampaignsByStatus_Result> getCampaignsByStatus();
        IEnumerable<GetLiveCampaignCountOverTime_Result> GetLiveCampaignCountOverTime(int CampaignType, DateTime DateFrom, DateTime DateTo, int Granularity);
       
    }
}
