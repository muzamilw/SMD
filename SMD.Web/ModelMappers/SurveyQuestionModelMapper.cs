using SMD.MIS.Areas.Api.Models;
using System.Linq;
using System.Web;
using SMD.Models.DomainModels;
using SurveyQuestion = SMD.MIS.Areas.Api.Models.SurveyQuestion;
using System.Collections.Generic;
using SMD.Models.Common;

namespace SMD.MIS.ModelMappers
{

    public static class SurveyQuestionModelMapper
    {
        /// <summary>
        /// Domain API to WEB API | baqer
        /// </summary>
        public static SurveyApiModel CreateFrom(this GetSurveys_Result source)
        {
            return new SurveyApiModel
            {
                Question = source.Question,
                Description = source.Description,
                ApprovalDate = source.ApprovalDate,
                DisplayQuestion = source.DisplayQuestion,
                LeftPicturePath = source.LeftPicturePath,
                ResultClicks = source.ResultClicks,
                RightPicturePath = source.RightPicturePath,
                SQID = source.SQID,
                VoucherCode = source.VoucherCode
            };
        }

        /// <summary>
        /// Domain resposne to Web resposne | API | baqer
        /// </summary>
        public static SurveyForApiSearchResponse CreateFrom(
            this Models.ResponseModels.SurveyForApiSearchResponse source)
        {
            return new SurveyForApiSearchResponse
            {
                Surveys = source.Surveys.Select(survey => survey.CreateFrom())
            };
        }


        /// <summary>
        /// Domain Search Response to Web Response | baqer
        /// </summary>
        public static SurveyQuestionRequestResponseModel CreateFrom(
            this Models.ResponseModels.SurveyQuestionResponseModel source)
        {
            return new SurveyQuestionRequestResponseModel
            {
                TotalCount = source.TotalCount,
                SurveyQuestions = source.SurveyQuestions.Select(question => question.CreateFrom()),
                CountryDropdowns =source.Countries.Select(country => country.CreateFrom()),
                LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom()) ,
                objBaseData = source.objBaseData.CreateFrom(),
                setupPrice = source.setupPrice
            };
        }

        /// <summary>
        /// Domain To Web  | baqer
        /// </summary>
        public static SurveyQuestion CreateFrom(this Models.DomainModels.SurveyQuestion source)
        {
            string leftPath = source.LeftPicturePath;
            if (source.LeftPicturePath !=  null &&  !source.LeftPicturePath.Contains("http"))
            {
                leftPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LeftPicturePath;
            }
            string rightPath = source.RightPicturePath;
            if (source.RightPicturePath != null && !source.RightPicturePath.Contains("http"))
            {
                rightPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.RightPicturePath;
            }
            return new SurveyQuestion
            {
                SqId = source.SqId,
                LanguageId = source.LanguageId,
                CountryId = source.CountryId,
                Type = source.Type,
                UserId = source.UserId,
                Status = source.Status,
                Question = source.Question,
                Description = source.Description,
                RepeatPeriod = source.RepeatPeriod,
                DisplayQuestion = source.DisplayQuestion,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Approved = source.Approved,
                ApprovalDate = source.ApprovalDate,
                CreationDate = source.CreationDate,
                ModifiedDate = source.ModifiedDate,
                LeftPicturePath = leftPath,
                RightPicturePath = rightPath,
                DiscountVoucherApplied = source.DiscountVoucherApplied,
                VoucherCode = source.VoucherCode,
                DiscountVoucherId = source.DiscountVoucherId,
                RejectionReason = source.RejectionReason,
                SubmissionDate = source.SubmissionDate,
                CreatedBy = source.User.FullName,
                CreatorAddress= source.User.State + " "+ source.User.Address1
                
            };
        }


        /// <summary>
        /// Web to Domain | baqer
        /// </summary>
        public static Models.DomainModels.SurveyQuestion CreateFrom(this SurveyQuestion source)
        {
            return new Models.DomainModels.SurveyQuestion
            {
                SqId = source.SqId,
                LanguageId = source.LanguageId,
                CountryId = source.CountryId,
                Type = source.Type,
                UserId = source.UserId,

                Question = source.Question,
                Description = source.Description,
                RepeatPeriod = source.RepeatPeriod,
                DisplayQuestion = source.DisplayQuestion,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Approved = source.Approved,
                ApprovalDate = source.ApprovalDate,
                CreationDate = source.CreationDate,
                ModifiedDate = source.ModifiedDate,
                LeftPicturePath = source.LeftPicturePath,
                RightPicturePath = source.RightPicturePath,
                DiscountVoucherApplied = source.DiscountVoucherApplied,
                VoucherCode = source.VoucherCode,
                DiscountVoucherId = source.DiscountVoucherId,
                RejectionReason = source.RejectionReason,
                SubmissionDate = source.SubmissionDate,
            };
        }

        /// <summary>
        /// Domain to Web mapper for Rejected Survey Questions | baqer
        /// </summary>
        public static SurveyQuestionResposneModelForAproval CreateFrom(
            this Models.ResponseModels.SurveyQuestionResposneModelForAproval source)
        {
            return new SurveyQuestionResposneModelForAproval
            {
                TotalCount = source.TotalCount,
                SurveyQuestions = source.SurveyQuestions.Select(question => question.CreateFrom())
            };
        }

        /// <summary>
        /// Returns user count  | baqer
        /// </summary>
        public static AudienceSurveyForApiResponse CreateFrom(this long count)
        {
            return new AudienceSurveyForApiResponse
            {
                UserCount = count
            };
        }

        /// <summary>
        /// Create DD from Domain Model
        /// </summary>
        public static SurveyQuestionDropDown CreateFromDropdown(this Models.DomainModels.SurveyQuestion source)
        {
            string leftPath = source.LeftPicturePath;
            if (source.LeftPicturePath != null && !source.LeftPicturePath.Contains("http"))
            {
                leftPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LeftPicturePath;
            }
            string rightPath = source.RightPicturePath;
            if (source.RightPicturePath != null && !source.RightPicturePath.Contains("http"))
            {
                rightPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.RightPicturePath;
            }
            return new SurveyQuestionDropDown
            {
                SQID = source.SqId,
                DisplayQuestion = source.DisplayQuestion,
                LeftPicturePath = leftPath,
                RightPicturePath = rightPath
            };
        }
        public static SMD.MIS.Areas.Api.Models.UserBaseData CreateFrom(this Models.Common.UserBaseData source)
        {
            if (source != null)
            {
                return new SMD.MIS.Areas.Api.Models.UserBaseData
                {
                    CityId = source.CityId,
                    CountryId = source.CountryId,
                    LanguageId = source.LanguageId,
                    IndustryId = source.IndustryId,
                    EducationId = source.EducationId,
                    City = source.City,
                    Country = source.Country,
                    Language = source.Language,
                    Industry = source.Industry,
                    Education = source.Education,
                    CurrencySymbol = source.CurrencySymbol
                };
            }
            else
            {
                return null;
            }
        }
    }

    public static class SurveyQuestionEditorModelMapper
    {
        /// <summary>
        /// Domain Search Response to Web Response 
        /// </summary>
        public static SurveyQuestionEditorRequestResponseModel CreateFromWithRef(
            this Models.ResponseModels.SurveyQuestionEditResponseModel source)
        {
            return new SurveyQuestionEditorRequestResponseModel
            {
                SurveyQuestion = source.SurveyQuestionObj.CreateFromWithReference(),
            };
        }

        /// <summary>
        /// Domain To Web 
        /// </summary>
        public static SurveyQuestion CreateFromWithReference(this Models.DomainModels.SurveyQuestion source)
        {
            string leftPath = source.LeftPicturePath;
            if (source.LeftPicturePath != null && !source.LeftPicturePath.Contains("http"))
            {
                leftPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LeftPicturePath;
            }
            string rightPath = source.RightPicturePath;
            if (source.RightPicturePath != null && !source.RightPicturePath.Contains("http"))
            {
                rightPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.RightPicturePath;
            }
            List<SMD.MIS.Areas.Api.Models.SurveyQuestionTargetCriteria> criterias = GetSurveyQuestionTargetCriterias(source);
            List<SMD.MIS.Areas.Api.Models.SurveyQuestionTargetLocation> locs = GetSurveyLocations(source);
            return new SurveyQuestion
            {
                SqId = source.SqId,
                LanguageId = source.LanguageId,
                CountryId = source.CountryId,
                Type = source.Type,
                UserId = source.UserId,
                Status = source.Status,
                Question = source.Question,
                Description = source.Description,
                RepeatPeriod = source.RepeatPeriod,
                DisplayQuestion = source.DisplayQuestion,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Approved = source.Approved,
                ApprovalDate = source.ApprovalDate,
                CreationDate = source.CreationDate,
                ModifiedDate = source.ModifiedDate,
                LeftPicturePath = leftPath,
                RightPicturePath = rightPath,
                DiscountVoucherApplied = source.DiscountVoucherApplied,
                VoucherCode = source.VoucherCode,
                DiscountVoucherId = source.DiscountVoucherId,
                RejectionReason = source.RejectionReason,
                SubmissionDate = source.SubmissionDate,
                SurveyQuestionTargetCriterias = criterias,
                SurveyQuestionTargetLocations = locs,
                AgeRangeEnd = source.AgeRangeEnd,
                AgeRangeStart = source.AgeRangeStart,
                Gender = source.Gender
            };
        }
        public static List<SMD.MIS.Areas.Api.Models.SurveyQuestionTargetCriteria> GetSurveyQuestionTargetCriterias(this Models.DomainModels.SurveyQuestion source)
        {
            List<SMD.MIS.Areas.Api.Models.SurveyQuestionTargetCriteria> result = new List<Areas.Api.Models.SurveyQuestionTargetCriteria>();

            foreach(var criteria in source.SurveyQuestionTargetCriterias)
            {
                SMD.MIS.Areas.Api.Models.SurveyQuestionTargetCriteria modelCriteria = new Areas.Api.Models.SurveyQuestionTargetCriteria();
                modelCriteria.Id = criteria.Id;
                modelCriteria.IncludeorExclude = criteria.IncludeorExclude;
                modelCriteria.IndustryId = criteria.IndustryId;
                modelCriteria.LanguageId = criteria.LanguageId;
                modelCriteria.LinkedSqAnswer = criteria.LinkedSqAnswer;
                modelCriteria.LinkedSqId = criteria.LinkedSqId;
                modelCriteria.PqAnswerId = criteria.PqAnswerId;
                modelCriteria.PqId = criteria.PqId;
                modelCriteria.SqId = criteria.SqId;
                modelCriteria.Type = criteria.Type;
                modelCriteria.EducationId = criteria.EducationId;
                if (criteria.Type == (int)SurveyQuestionTargetCriteriaType.ProfileQuestion)
                {
                    if (criteria.ProfileQuestion != null)
                    {
                        modelCriteria.questionString = criteria.ProfileQuestion.Question;
                        if(criteria.ProfileQuestionAnswer != null)
                            modelCriteria.answerString = criteria.ProfileQuestionAnswer.AnswerString;
                    }
                }
                else if (criteria.Type == (int)SurveyQuestionTargetCriteriaType.SurveryQuestion)
                {
                    if (criteria.LinkedSurveyQuestion != null)
                    {
                        modelCriteria.questionString = criteria.LinkedSurveyQuestion.DisplayQuestion;
                        string pictureUrl = criteria.LinkedSurveyQuestion.RightPicturePath;
                        if (criteria.LinkedSqAnswer == (int)SurveyQuestionAnswerType.Left)
                        {
                            pictureUrl = criteria.LinkedSurveyQuestion.LeftPicturePath;
                        }

                        if (pictureUrl != null && !pictureUrl.Contains("http"))
                        {
                            pictureUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + pictureUrl;
                        }
                        modelCriteria.answerString = pictureUrl;
                    }
                }
                else if (criteria.Type == (int)SurveyQuestionTargetCriteriaType.Language)
                {
                    if (criteria.Language != null)
                        modelCriteria.Language = criteria.Language.LanguageName;
                }
                else if (criteria.Type == (int)SurveyQuestionTargetCriteriaType.Industry)
                {
                    if (criteria.Industry != null)
                    {
                        modelCriteria.Industry = criteria.Industry.IndustryName;
                    }
                }
                else if (criteria.Type == (int)SurveyQuestionTargetCriteriaType.Education)
                {
                    if (criteria.Education != null)
                    {
                        modelCriteria.Education = criteria.Education.Title;
                    }
                }
                result.Add(modelCriteria);
            }
            return result;
        }
        public static List<SMD.MIS.Areas.Api.Models.SurveyQuestionTargetLocation> GetSurveyLocations(this Models.DomainModels.SurveyQuestion source)
        {
            List<SMD.MIS.Areas.Api.Models.SurveyQuestionTargetLocation> result = new List<Areas.Api.Models.SurveyQuestionTargetLocation>();
            foreach(var location in source.SurveyQuestionTargetLocations)
            {
                SMD.MIS.Areas.Api.Models.SurveyQuestionTargetLocation modelLocation = new Areas.Api.Models.SurveyQuestionTargetLocation();
                modelLocation.CityId = location.CityId;
                modelLocation.CountryId = location.CountryId;
                if (location.City != null)
                {
                    modelLocation.City = location.City.CityName;
                    modelLocation.Latitude = location.City.GeoLat;
                    modelLocation.Longitude = location.City.GeoLong;
                }
                if(location.Country != null)
                    modelLocation.Country = location.Country.CountryName;
                modelLocation.Id = location.Id;
                modelLocation.IncludeorExclude = location.IncludeorExclude;
                modelLocation.Radius = location.Radius;
                modelLocation.SqId = location.SqId;
                result.Add(modelLocation);
            }
            return result.OrderBy(g=>g.CountryId).ToList();
        }
    }
}