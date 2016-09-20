using System.Web;
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

            string Voucherpath = "";
            if (source.ImagePath != null && !source.ImagePath.Contains("http"))
            {
                path = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.ImagePath;
            }
            string LandingPageVideoLinkAsPath = "";
            string LandingPageVideoLink = source.LandingPageVideoLink;
            if (source.Type == (int)AdCampaignType.Other && !string.IsNullOrEmpty(source.LandingPageVideoLink))
            {
                LandingPageVideoLinkAsPath = source.LandingPageVideoLink;
                //if (LandingPageVideoLink != null && !LandingPageVideoLink.Contains("http"))
                //{
                //    LandingPageVideoLinkAsPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LandingPageVideoLink;
                //}
                //if (LandingPageVideoLink != null && !LandingPageVideoLink.Contains("http"))
                //{
                //    LandingPageVideoLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LandingPageVideoLink;
                //}

            }


            if (source.Voucher1ImagePath != null && !source.Voucher1ImagePath.Contains("http"))
            {
                Voucherpath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.Voucher1ImagePath;
            }
            return new AdCampaign
            {
                CampaignId = source.CampaignId,
                CreatedBy = source.Company != null ? source.Company.CompanyName : source.CreatedBy,
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
                Voucher2Value = source.Voucher2Value,
                Voucher1ImagePath = source.Voucher1ImagePath,
                VoucherImagePath = Voucherpath,
                VideoUrl = source.VideoUrl,
                BuuyItLine1 = source.BuuyItLine1,
                BuyItButtonLabel = source.BuyItButtonLabel,
                BuyItImageUrl = source.BuyItImageUrl,
                BuyItLine2 = source.BuyItLine2,
                BuyItLine3 = source.BuyItLine3,
                AdViews = source.AdViews,
                CompanyId = source.CompanyId,
                CouponActualValue = source.CouponActualValue,
                CouponQuantity = source.CouponQuantity,
                CouponSwapValue = source.CouponSwapValue,
                CouponTakenCount = source.CouponTakenCount,
                priority = source.priority,
                CouponDiscountValue = source.CouponDiscountValue,
                CouponExpiryLabel = source.CouponExpiryLabel,
                couponImage2 = source.couponImage2,
                CouponCategories = source.CampaignCategories != null ? source.CampaignCategories.Select(x => x.CouponCategory.CreateFrom()).ToList() : null,

                couponSmdComission = source.couponSmdComission,
                CouponImage3 = source.CouponImage3,
                CouponImage4 = source.CouponImage4,
               // CouponCodes = source.CouponCodes != null ? source.CouponCodes.Select(x => x.CreateFrom()).ToList() : null,
                IsUseFilter = source.IsUseFilter == true ? 1 : 0,
                LogoUrl = source.LogoUrl == null ? "" : source.LogoUrl,
                VoucherAdditionalInfo = source.VoucherAdditionalInfo == null ? "" : source.VoucherAdditionalInfo,
                CouponId = source.CouponId ?? 0,
                IsShowVoucherSetting = source.IsShowVoucherSetting ?? false,
                VideoLink2 = source.VideoLink2,
                CouponType = source.CouponType ?? 1,
                IsSavedCoupon = source.IsSavedCoupon ?? false,
                DeliveryDays = source.DeliveryDays ?? 1,
                MaxDailyBudget = source.MaxDailyBudget ?? 0,
                SubmissionDateTime =source.SubmissionDateTime
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
                Campaigns =source.Campaign != null? source.Campaign.Select(campaign => campaign.CreateFrom()):null,
                Coupon = source.Coupon != null? source.Coupon.Select(coupon =>coupon.CreateFrom()):null
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
            else if (source.Type == (int)AdCampaignCriteriaType.QuizQustion)
            {
                if (source.QuizCampaign != null)
                {
                    QuestionString = source.QuizCampaign.VerifyQuestion;
                    if (source.QuizAnswerId == 1)
                        AnswerString = source.QuizCampaign.Answer1;
                    if (source.QuizAnswerId == 2)
                        AnswerString = source.QuizCampaign.Answer2;
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
                Education = EducationName,
                QuizAnswerId = source.QuizAnswerId,
                QuizCampaignId = source.QuizCampaignId
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
                GeoLong = source.GeoLong,
                BuyItClausePrice = source.BuyItClausePrice,
                FiveDayDeliveryClausePrice = source.FiveDayDeliveryClausePrice,
                QuizQuestionClausePrice = source.QuizQuestionClausePrice,
                TenDayDeliveryClausePrice = source.TenDayDeliveryClausePrice,
                ThreeDayDeliveryClausePrice = source.ThreeDayDeliveryClausePrice,
                UserProfileImage = source.UserProfileImage,
                VoucherClausePrice = source.VoucherClausePrice
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
                Professions = source.Industry.Select(ind => ind.CreateFrom()),
                CouponCategories = source.CouponCategory.Select(cc => cc.CreateFromForCategories(false)),
                DiscountVouchers = source.DiscountVouchers == null? null: source.DiscountVouchers.Select(dv => dv.CreateFromDiscountVoucher()),
                Currencies = source.Currencies == null? null: source.Currencies.Select(cr =>cr.CreateFrom())
            };
        }

        public static SMD.MIS.Areas.Api.Models.Coupon CreateFrom(this Models.DomainModels.Coupon source)
        {
            //string path = source.ImagePath;

            //string Voucherpath = "";
            //if (source.ImagePath != null && !source.ImagePath.Contains("http"))
            //{
            //    path = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.ImagePath;
            //}
            //string LandingPageVideoLinkAsPath = "";
            //string LandingPageVideoLink = source.LandingPageVideoLink;
            //if (source.Type == (int)AdCampaignType.Other && !string.IsNullOrEmpty(source.LandingPageVideoLink))
            //{
            //    LandingPageVideoLinkAsPath = source.LandingPageVideoLink;
            //    //if (LandingPageVideoLink != null && !LandingPageVideoLink.Contains("http"))
            //    //{
            //    //    LandingPageVideoLinkAsPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LandingPageVideoLink;
            //    //}
            //    //if (LandingPageVideoLink != null && !LandingPageVideoLink.Contains("http"))
            //    //{
            //    //    LandingPageVideoLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LandingPageVideoLink;
            //    //}

            //}


            //if (source.Voucher1ImagePath != null && !source.Voucher1ImagePath.Contains("http"))
            //{
            //    Voucherpath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.Voucher1ImagePath;
            //}
            return new SMD.MIS.Areas.Api.Models.Coupon
            {
                ApprovalDateTime = source.ApprovalDateTime,
                Approved = source.Approved,
                ApprovedBy = source.ApprovedBy,
                Archived = source.Archived,
                CompanyId = source.CompanyId,
                CouponActiveMonth = source.CouponActiveMonth,
                CouponActiveYear = source.CouponActiveYear,
                CouponExpirydate = source.CouponExpirydate,
                CouponId = source.CouponId,
                couponImage1 = source.couponImage1,
                CouponImage2 = source.CouponImage2,
                CouponImage3 = source.CouponImage3,
                CouponIssuedCount = source.CouponIssuedCount,
                CouponListingMode = source.CouponListingMode,
                CouponQtyPerUser = source.CouponQtyPerUser,
                CouponRedeemedCount = source.CouponRedeemedCount,
                CouponTitle = source.CouponTitle,
                CouponViewCount = source.CouponViewCount,
                CreatedBy = source.CreatedBy,
                CreatedDateTime = source.CreatedDateTime,
                CurrencyId = source.CurrencyId,
                FinePrintLine1 = source.FinePrintLine1,
                FinePrintLine2 = source.FinePrintLine2,
                FinePrintLine3 = source.FinePrintLine3,
                FinePrintLine4 = source.FinePrintLine4,
                FinePrintLine5 = source.FinePrintLine5,
                GeographyColumn = source.GeographyColumn,
                HighlightLine1 = source.HighlightLine1,
                HighlightLine2 = source.HighlightLine2,
                HighlightLine3 = source.HighlightLine3,
                HighlightLine4 = source.HighlightLine4,
                HighlightLine5 = source.HighlightLine5,
                HowToRedeemLine1 = source.HowToRedeemLine1,
                HowToRedeemLine2 = source.HowToRedeemLine2,
                HowToRedeemLine3 = source.HowToRedeemLine3,
                HowToRedeemLine4 = source.HowToRedeemLine4,
                HowToRedeemLine5 = source.HowToRedeemLine5,
                LanguageId = source.LanguageId,
                LocationBranchId = source.LocationBranchId,
                LocationCity = source.LocationCity,
                LocationLAT = source.LocationLAT,
                LocationLine1 = source.LocationLine1,
                LocationLine2 = source.LocationLine2,
                LocationLON = source.LocationLON,
                LocationPhone = source.LocationPhone,
                LocationState = source.LocationState,
                LocationTitle = source.LocationTitle,
                LocationZipCode = source.LocationZipCode,
                LogoUrl = source.LogoUrl[0] != '/' ? "/" + source.LogoUrl : source.LogoUrl,
                ModifiedBy = source.ModifiedBy,
                ModifiedDateTime = source.ModifiedDateTime,
                Price = source.Price,
                RejectedBy = source.RejectedBy,
                Rejecteddatetime = source.Rejecteddatetime,
                RejectedReason = source.RejectedReason,
                Savings = source.Savings,
                SearchKeywords = source.SearchKeywords,
                Status = source.Status,
                SwapCost = source.SwapCost,
                UserId = source.UserId,
                SubmissionDateTime=source.SubmissionDateTime,
                CouponCategories = source.CouponCategories != null ? source.CouponCategories.Select(coupon => coupon.CreateFrom()) : null,
                CouponStartDate=source.CouponStartDate,
                CouponEndDate=source.CouponEndDate,
                Priority=source.Priority
              };


        }
        public static SMD.MIS.Areas.Api.Models.CouponCategories CreateFrom(this Models.DomainModels.CouponCategories source)
        {
            return new SMD.MIS.Areas.Api.Models.CouponCategories
            {
                CategoryId = source.CategoryId,
                CouponId = source.CouponId,
                Id = source.Id
            };
        }
    }
}