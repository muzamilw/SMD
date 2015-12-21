﻿using System.Web;
using Excel;
using SMD.MIS.Areas.Api.Models;
using System.Linq;
using SMD.Models.DomainModels;
using AdCampaign = SMD.MIS.Areas.Api.Models.AdCampaign;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Ad Campaign Mapper
    /// </summary>
    public static class AdCampaignMapper
    {
        /// <summary>
        /// Domain Search Response to Web Response |baqer
        /// </summary>
        public static AdCampaignResposneModelForAproval CreateFrom(
            this Models.ResponseModels.AdCampaignResposneModelForAproval source)
        {
            return new AdCampaignResposneModelForAproval
            {
                TotalCount = source.TotalCount,
                AdCampaigns = source.AdCampaigns.Select(ad => ad.CreateFrom()).ToList()
            };
        }

        /// <summary>
        /// Domain to Web Mapper |baqer
        /// </summary>
        public static AdCampaign CreateFrom(this Models.DomainModels.AdCampaign source)
        {
            string path = source.ImagePath;

            if (source.ImagePath != null && !source.ImagePath.Contains("http"))
            {
                path = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.ImagePath;
            }
            return new AdCampaign
            {
                CampaignId = source.CampaignId,
                CreatedBy = source.CreatedBy,
                LanguageId = source.LanguageId,
                Type = source.Type,
                Status = source.Status,
                ImagePath = path,
                AgeRangeEnd = source.AgeRangeEnd,
                AgeRangeStart = source.AgeRangeStart,
                AmountSpent = source.AmountSpent,
                Answer1 = source.Answer1,
                Answer2 = source.Answer2,
                Answer3 = source.Answer3,
                ApprovalDateTime = source.ApprovalDateTime,
                Approved = source.Approved,
                ApprovedBy = source.ApprovedBy,
                Archived = source.Archived,
                CampaignDescription = source.CampaignDescription,
                CampaignName = source.CampaignName,
                ClickRate = source.ClickRate,
                CorrectAnswer = source.CorrectAnswer,
                CreatedDateTime = source.CreatedDateTime,
                Description = source.Description,
                DisplayTitle = source.DisplayTitle,
                SmdCampaign = source.SmdCampaign,
                EndDateTime = source.EndDateTime,
                Gender = source.Gender,
                LandingPageVideoLink = source.LandingPageVideoLink,
                MaxBudget = source.MaxBudget,
                ModifiedBy = source.ModifiedBy,
                ModifiedDateTime = source.ModifiedDateTime,
                ProjectedReach = source.ProjectedReach,
                RejectedReason = source.RejectedReason,
                ResultClicks = source.ResultClicks,
                SmdCredits = source.SmdCredits,
                StartDateTime = source.StartDateTime,
                UserId = source.UserId,
                VerifyQuestion = source.VerifyQuestion,
                
                AdCampaignTargetCriterias =
                    source.AdCampaignTargetCriterias != null ? source.AdCampaignTargetCriterias.Select(x => x.CreateFrom()).ToList() : null,

                AdCampaignTargetLocations =
                    source.AdCampaignTargetLocations != null ? source.AdCampaignTargetLocations.Select(x => x.CreateFrom()).ToList() : null,
            };


        }

        /// <summary>
        /// Web To Domain Mapper |baqer
        /// </summary>
        public static Models.DomainModels.AdCampaign CreateFrom(this AdCampaign source)
        {
            return new Models.DomainModels.AdCampaign
            {
                CampaignId = source.CampaignId,
                CreatedBy = source.CreatedBy,
                LanguageId = source.LanguageId,
                Type = source.Type,
                Status = source.Status,
                ImagePath = source.ImagePath,
                AgeRangeEnd = source.AgeRangeEnd,
                AgeRangeStart = source.AgeRangeStart,
                AmountSpent = source.AmountSpent,
                Answer1 = source.Answer1,
                Answer2 = source.Answer2,
                Answer3 = source.Answer3,
                ApprovalDateTime = source.ApprovalDateTime,
                Approved = source.Approved,
                ApprovedBy = source.ApprovedBy,
                Archived = source.Archived,
                CampaignDescription = source.CampaignDescription,
                CampaignName = source.CampaignName,
                ClickRate = source.ClickRate,
                CorrectAnswer = source.CorrectAnswer,
                CreatedDateTime = source.CreatedDateTime,
                Description = source.Description,
                DisplayTitle = source.DisplayTitle,
                SmdCampaign = source.SmdCampaign,
                EndDateTime = source.EndDateTime,
                Gender = source.Gender,
                LandingPageVideoLink = source.LandingPageVideoLink,
                MaxBudget = source.MaxBudget,
                ModifiedBy = source.ModifiedBy,
                ModifiedDateTime = source.ModifiedDateTime,
                ProjectedReach = source.ProjectedReach,
                RejectedReason = source.RejectedReason,
                ResultClicks = source.ResultClicks,
                SmdCredits = source.SmdCredits,
                StartDateTime = source.StartDateTime,
                UserId = source.UserId,
                VerifyQuestion = source.VerifyQuestion
            };


        }

        /// <summary>
        /// Domain Search Response to Web Response  |baqer
        /// </summary>
        public static CampaignRequestResponseModel CreateCampaignFrom(
            this Models.ResponseModels.CampaignResponseModel source)
        {
            return new CampaignRequestResponseModel
            {
                TotalCount = source.TotalCount,
                Campaigns = source.Campaign.Select(campaign => campaign.CreateFrom()),
                LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom())
            };
        }

        /// <summary>
        /// Domain to Web Resposne For API  |baqer
        /// </summary>
        public static AdCampaignApiSearchRequestResponse CreateResponseForApi(
            this Models.ResponseModels.AdCampaignApiSearchRequestResponse source)
        {
            return new AdCampaignApiSearchRequestResponse
            {
                AdCampaigns = source.AdCampaigns.Select(ad => ad.CreateApiModel())
            };
        }

        /// <summary>
        /// Domain APi to web APi  |baqer
        /// </summary>
        public static AdCampaignApiModel CreateApiModel(this GetAds_Result source)
        {
            return new AdCampaignApiModel
            {
                Answer1 = source.Answer1,
                Answer2 = source.Answer2,
                Answer3 = source.Answer3,
                CampaignId = source.CampaignID,
                CampaignName = source.CampaignName,
                ClickRate = source.ClickRate,
                CorrectAnswer = source.CorrectAnswer,
                Description = source.Description,
                LandingPageVideoLink = source.LandingPageVideoLink,
                VerifyQuestion = source.VerifyQuestion
            };
        }

        /// <summary>
        /// Return User Count  |baqer
        /// </summary>
        public static AudienceAdCampaignForApiResponse CreateCountFrom(this long source)
        {
            return new AudienceAdCampaignForApiResponse
            {
                UserCount = source
            };
        }
        /// <summary>
        /// Domain to Web Mapper
        /// </summary>
        public static AdCampaignTargetCriteria CreateFrom(this Models.DomainModels.AdCampaignTargetCriteria source)
        {

            return new AdCampaignTargetCriteria
            {
                CriteriaId = source.CriteriaId,
                CampaignId = source.CampaignId,
                IncludeorExclude = source.IncludeorExclude,
                IndustryId = source.IndustryId,
                LanguageId = source.LanguageId,
                PqAnswerId = source.PqAnswerId,
                PqId = source.PqId,
                SqAnswer = source.SqAnswer,
                SqId = source.SqId,
                Type = source.Type
            };
        }

        /// <summary>
        /// Domain to Web Mapper
        /// </summary>
        public static AdCampaignTargetLocation CreateFrom(this Models.DomainModels.AdCampaignTargetLocation source)
        {

            return new AdCampaignTargetLocation
            {
                Id = source.Id,
                CampaignId = source.CampaignId,
                CityId = source.CityId,
                CountryId = source.CountryId,
                IncludeorExclude = source.IncludeorExclude,
                Radius = source.Radius

            };
        }
    }
}