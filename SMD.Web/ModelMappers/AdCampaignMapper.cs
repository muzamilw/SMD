﻿using System.Web;
using Excel;
using SMD.MIS.Areas.Api.Models;
using System.Linq;
using SMD.Models.DomainModels;
using AdCampaign = SMD.MIS.Areas.Api.Models.AdCampaign;
using SMD.Models.Common;

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
            string LandingPageVideoLinkAsPath = "";
            string LandingPageVideoLink = source.LandingPageVideoLink;
            if (source.Type == (int)AdCampaignType.Other && !string.IsNullOrEmpty(source.LandingPageVideoLink))
            {
                LandingPageVideoLinkAsPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LandingPageVideoLink;
                LandingPageVideoLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LandingPageVideoLink;
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
                DisplayTitle = source.DisplayTitle,
                SmdCampaign = source.SmdCampaign,
                EndDateTime = source.EndDateTime,
                Gender = source.Gender,
                LandingPageVideoLink = LandingPageVideoLink,
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
                CampaignImagePath = path,
                CampaignTypeImagePath = LandingPageVideoLinkAsPath,
                Description = source.Description,
                AdCampaignTargetCriterias =
                    source.AdCampaignTargetCriterias != null ? source.AdCampaignTargetCriterias.Select(x => x.CreateFrom()).ToList() : null,

                AdCampaignTargetLocations =
                    source.AdCampaignTargetLocations != null ? source.AdCampaignTargetLocations.Select(x => x.CreateFrom()).ToList() : null,
                Voucher1Description = source.Voucher1Description,
                Voucher1Heading = source.Voucher1Heading,
                Voucher1Value = source.Voucher1Value,
                Voucher2Description = source.Voucher2Description,
                Voucher2Heading = source.Voucher2Heading,
                Voucher2Value = source.Voucher2Value

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
                VerifyQuestion = source.VerifyQuestion,

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
                //LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom()),
                //UserAndCostDetails = source.UserAndCostDetails.CreateFrom()
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
                VerifyQuestion = source.VerifyQuestion,
                Type = source.AdType
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
        public static SMD.MIS.Areas.Api.Models.AdCampaignTargetCriteria CreateFrom(this Models.DomainModels.AdCampaignTargetCriteria source)
        {
            string QuestionString = "";
            string AnswerString = "";
            string LanguageName = "";
            string IndustryName = "";
            string EducationName = "";
            if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.ProfileQuestion)
            {
                if (source.PqId != null && source.PqId > 0 && source.ProfileQuestion != null)
                {
                    QuestionString = source.ProfileQuestion.Question;
                }
                if (source.PqAnswerId != null && source.PqAnswerId > 0 && source.ProfileQuestionAnswer != null)
                {
                    AnswerString = source.ProfileQuestionAnswer.AnswerString;
                }
            }
            else if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.SurveyQuestion)
            {
                if (source.SqId != null && source.SqId > 0 && source.SurveyQuestion != null)
                {
                    QuestionString = source.SurveyQuestion.Question;
                }
                if (source.SqAnswer != null && source.SqAnswer > 0 && source.SurveyQuestion != null)
                {
                    if (source.SqAnswer == 1)
                    {
                        if (!source.SurveyQuestion.LeftPicturePath.Contains("http:"))
                        {
                            AnswerString = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.SurveyQuestion.LeftPicturePath;
                        }
                        else
                        {
                            AnswerString = source.SurveyQuestion.LeftPicturePath;
                        }

                    }
                    else
                    {
                        if (!source.SurveyQuestion.RightPicturePath.Contains("http:"))
                        {
                            AnswerString = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.SurveyQuestion.RightPicturePath;
                        }
                        else
                        {
                            AnswerString = source.SurveyQuestion.RightPicturePath;
                        }

                    }

                }
            }
            else if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.Language)
            {
                if (source.LanguageId != null && source.LanguageId > 0 && source.Language != null)
                {
                    LanguageName = source.Language.LanguageName;
                }
            }
            else if (source.Type == (int)SurveyQuestionTargetCriteriaType.Industry)
            {
                if (source.Industry != null)
                {
                    IndustryName = source.Industry.IndustryName;
                }
            }
            else if (source.Type == (int)SurveyQuestionTargetCriteriaType.Education)
            {
                if (source.Education != null)
                {
                    EducationName = source.Education.Title;
                }
            }
            return new SMD.MIS.Areas.Api.Models.AdCampaignTargetCriteria
            {
                CriteriaId = source.CriteriaId,
                CampaignId = source.CampaignId,
                IncludeorExclude = source.IncludeorExclude,
                IndustryId = source.IndustryId,
                LanguageId = source.LanguageId,
                PQAnswerId = source.PqAnswerId,
                PQId = source.PqId,
                SQAnswer = source.SqAnswer,
                SQId = source.SqId,
                Type = source.Type,
                questionString = QuestionString,
                answerString = AnswerString,
                Language = LanguageName,
                Industry = IndustryName,
                EducationId = source.EducationId,
                Education = EducationName
            };
        }

        /// <summary>
        /// Domain to Web Mapper
        /// </summary>
        public static SMD.MIS.Areas.Api.Models.AdCampaignTargetLocation CreateFrom(this Models.DomainModels.AdCampaignTargetLocation source)
        {
            string CName = "";
            string CountName = "";
            string latitdue = "";
            string longitude = "";
            if (source.CountryId != null && source.CountryId > 0 && source.Country != null)
            {
                CountName = source.Country.CountryName;

            }
            if (source.CityId != null && source.CityId > 0 && source.City != null)
            {
                CName = source.City.CityName;
                latitdue = source.City.GeoLat;
                longitude = source.City.GeoLong;
            }
            return new SMD.MIS.Areas.Api.Models.AdCampaignTargetLocation
            {
                Id = source.Id,
                CampaignId = source.CampaignId,
                CityId = source.CityId,
                CountryId = source.CountryId,
                IncludeorExclude = source.IncludeorExclude,
                Radius = source.Radius,
                City = CName,
                Country = CountName,
                Latitude = latitdue,
                Longitude = longitude
            };
        }


        /// <summary>
        /// User Cost and detail
        /// </summary>
        public static SMD.MIS.Areas.Api.Models.UserAndCostDetail CreateFrom(this SMD.Models.Common.UserAndCostDetail source)
        {


            return new SMD.MIS.Areas.Api.Models.UserAndCostDetail
            {
                AgeClausePrice = source.AgeClausePrice,
                CityId = source.CityId,
                CountryId = source.CountryId,
                EducationClausePrice = source.EducationClausePrice,
                EducationId = source.EducationId,
                GenderClausePrice = source.GenderClausePrice,
                IndustryId = source.IndustryId,
                LanguageId = source.LanguageId,
                LocationClausePrice = source.LocationClausePrice,
                OtherClausePrice = source.OtherClausePrice,
                ProfessionClausePrice = source.ProfessionClausePrice,
                City = source.CityName,
                Country = source.CountryName,
                Education = source.EducationTitle,
                Industry = source.IndustryName,
                Language = source.LanguageName,
                isStripeIntegrated = source.isStripeIntegrated,
                GeoLat = source.GeoLat,
                GeoLong = source.GeoLong
            };


        }
        /// <summary>
        /// Domain Search Response to Web Response 
        /// </summary>
        public static AdCampaignBaseResponse CreateCampaignBaseResponseFrom(
            this Models.ResponseModels.AdCampaignBaseResponse source)
        {
            return new AdCampaignBaseResponse
            {
                Languages = source.Languages.Select(lang => lang.CreateFrom()),
                UserAndCostDetails = source.UserAndCostDetails.CreateFrom(),
                Educations = source.Education.Select(edu => edu.CreateFrom()),
                Professions = source.Industry.Select(ind => ind.CreateFrom())
            };
        }
    }
}