using SMD.MIS.Areas.Api.Models;
using System.Linq;
using System.Web;
using SMD.Models.DomainModels;
using SurveyQuestion = SMD.MIS.Areas.Api.Models.SurveyQuestion;

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
                LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom()) 
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
        /// Create DD from Domain Model
        /// </summary>
        public static SurveyQuestionDropDown CreateFromDropdown(this Models.DomainModels.SurveyQuestion source)
        {
            string leftPath = source.LeftPicturePath;
            if (!source.LeftPicturePath.Contains("http"))
            {
                leftPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.LeftPicturePath;
            }
            string rightPath = source.RightPicturePath;
            if (!source.RightPicturePath.Contains("http"))
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
                SurveyQuestionTargetCriterias = source.SurveyQuestionTargetCriterias == null ? null : source.SurveyQuestionTargetCriterias.ToList(),
                SurveyQuestionTargetLocations = source.SurveyQuestionTargetLocations == null ? null : source.SurveyQuestionTargetLocations.ToList(),
                AgeRangeEnd = source.AgeRangeEnd,
                AgeRangeStart = source.AgeRangeStart,
                Gender = source.Gender
            };
        }
    }
}