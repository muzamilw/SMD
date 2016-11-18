﻿using SMD.Models.DomainModels;

namespace SMD.Interfaces.Repository
{
    public interface IAdCampaignResponseRepository : IBaseRepository<AdCampaignResponse, int>
    {
        /// <summary>
        /// Returns Users Response for Campaign
        /// </summary>
        AdCampaignResponse GetByUserId(long campaignId, string userId);
        int getCampaignByIdQQFormAnalytic(int CampaignId, int Choice, int Gender, int age, string Profession, string City);
    }
}
