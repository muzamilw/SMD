using SMD.Models.Common;
using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    /// <summary>
    /// Tax Repository Interface 
    /// </summary>
    public interface ICampaignEventHistoryRepository : IBaseRepository<CampaignEventHistory, long>
    {

        bool InsertCampaignEvent(AdCampaignStatus Event, long Campaignid);

        bool InsertCouponEvent(AdCampaignStatus Event, long CouponId);

        bool InsertSurveyQuestionEvent(AdCampaignStatus Event, long SQID);

        bool InsertProfileQuestionEvent(AdCampaignStatus Event, int PQID);

        
       
    }
}
