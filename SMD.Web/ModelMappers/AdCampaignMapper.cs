using System.Web;
using SMD.MIS.Areas.Api.Models;
using System.Linq;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Ad Campaign Mapper
    /// </summary>
    public static class AdCampaignMapper
    {
        /// <summary>
        /// Domain Search Response to Web Response 
        /// </summary>
        public static AdCampaignResposneModelForAproval CreateFrom(
            this Models.ResponseModels.AdCampaignResposneModelForAproval source)
        {
            return new AdCampaignResposneModelForAproval
            {
                TotalCount = source.TotalCount,
                AdCampaigns = source.AdCampaigns.Select(ad=> ad.CreateFrom()).ToList()
            };
        }

        /// <summary>
        /// Domain to Web Mapper
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
                VerifyQuestion = source.VerifyQuestion
            };


        }

        /// <summary>
        /// Web To Domain Mapper
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
        /// Domain Search Response to Web Response 
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
    }
}