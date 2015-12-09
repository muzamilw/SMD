using SMD.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{

    public static class SurveyQuestionModelMapper
    {
        /// <summary>
        /// Domain Search Response to Web Response 
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
        /// Domain To Web 
        /// </summary>
        public static SurveyQuestion CreateFrom(this Models.DomainModels.SurveyQuestion source)
        {
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
                LeftPicturePath = source.LeftPicturePath,
                RightPicturePath = source.RightPicturePath,
                DiscountVoucherApplied = source.DiscountVoucherApplied,
                VoucherCode = source.VoucherCode,
                DiscountVoucherId = source.DiscountVoucherId,
                RejectionReason = source.RejectionReason
            };
        }


        /// <summary>
        /// Web to Domain 
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
                RejectionReason = source.RejectionReason
            };
        }

     

    }
}