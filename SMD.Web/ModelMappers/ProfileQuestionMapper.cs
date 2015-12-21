using SMD.MIS.Areas.Api.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ProfileQuestionAnswer = SMD.Models.DomainModels.ProfileQuestionAnswer;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Profile Question Mapper
    /// </summary>
    public static class ProfileQuestionMapper
    {
        #region MIS
        /// <summary>
        /// Domain Search Response to Web Response 
        /// </summary>
        public static ProfileQuestionSearchRequestResponse CraeteFrom(
            this Models.ResponseModels.ProfileQuestionSearchRequestResponse source)
        {
            return new ProfileQuestionSearchRequestResponse
            {
                TotalCount = source.TotalCount,
                ProfileQuestions = source.ProfileQuestions.Select(question => question.CreateFrom())
            };
        }

        /// <summary>
        /// Domain To Web 
        /// </summary>
        public static ProfileQuestion CreateFrom(this Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestion
            {
                PqId = source.PqId,
                Question = source.Question,
                Priority = source.Priority,
                HasLinkedQuestions = source.HasLinkedQuestions,
                ProfileGroupName = source.ProfileQuestionGroup != null ? source.ProfileQuestionGroup.ProfileGroupName : null,

                LanguageId = source.LanguageId,
                CountryId = source.CountryId,
                ProfileGroupId = source.ProfileGroupId,
                Type = source.Type,
                RefreshTime = source.RefreshTime,
                SkippedCount = source.SkippedCount,
                CreationDate = source.CreationDate,
                ModifiedDate = source.ModifiedDate,
                PenalityForNotAnswering = source.PenalityForNotAnswering,
                Status = source.Status
            };
        }

        /// <summary>
        /// Web to Domain 
        /// </summary>
        public static Models.DomainModels.ProfileQuestion CreateFrom(this ProfileQuestion source)
        {
            return new Models.DomainModels.ProfileQuestion
            {
                PqId = source.PqId,
                Question = source.Question,
                Priority = source.Priority,
                HasLinkedQuestions = source.HasLinkedQuestions,

                LanguageId = source.LanguageId,
                CountryId = source.CountryId,
                ProfileGroupId = source.ProfileGroupId,
                Type = source.Type,
                RefreshTime = source.RefreshTime,
                SkippedCount = source.SkippedCount,
                CreationDate = source.CreationDate,
                ModifiedDate = source.ModifiedDate,
                PenalityForNotAnswering = source.PenalityForNotAnswering,
                Status = source.Status,
                ProfileQuestionAnswers = source.ProfileQuestionAnswers != null ? source.ProfileQuestionAnswers.Select(ans => ans.CreateFrom()).ToList() : new Collection<ProfileQuestionAnswer>().ToList()
            };
        }
        /// <summary>
        /// Create DD from Domain Model 
        /// </summary>
        public static ProfileQuestionDropdown CreateFromDropdown(this Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestionDropdown
            {
                PqId = source.PqId,
                Question = source.Question
            };
        }

        /// <summary>
        /// Creates web model of BaseData
        /// </summary>
        public static ProfileQuestionBaseResponse CreateFrom(
            this Models.ResponseModels.ProfileQuestionBaseResponse source)
        {
            return new ProfileQuestionBaseResponse
            {
                CountryDropdowns = source.Countries.Select(country => country.CreateFrom()),
                LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom()),
                ProfileQuestionGroupDropdowns = source.ProfileQuestionGroups.Select(group => group.CreateFrom()),
                ProfileQuestionDropdowns = source.ProfileQuestions.Select(question => question.CreateFromDropdown())
            };
        }

        #endregion
        #region APIs

        /// <summary>
        /// Domain to Api
        /// </summary>
        public static ProfileQuestionApiModel CreateForApi(this Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestionApiModel
            {
                PqId= source.PqId,
                Question = source.Question,
                QuestionType= source.Type,
                ProfileGroupName= source.ProfileQuestionGroup.ProfileGroupName,
                ProfileQuestionAnswers = source.ProfileQuestionAnswers.Select(ans => ans.CreateForApi()).ToList()
            };
        }

        /// <summary>
        /// Response to Apis Call
        /// </summary>
        public static ProfileQuestionApiSearchResponse CreateFrom(this Models.ResponseModels.ProfileQuestionApiSearchResponse source)
        {
            return new ProfileQuestionApiSearchResponse
            {
                ProfileQuestionApiModels = source.ProfileQuestions.Select(question => question.CreateForApi()),
                PercentageCompleted= source.PercentageCompleted
            };
        }

        /// <summary>
        /// Domain to API response 
        /// </summary>
        public static UpdateProfileQuestionUserAnswerResponse CreateFrom(this string source)
        {
            return new UpdateProfileQuestionUserAnswerResponse
            {
                Response = source
            };
        }
        #endregion 
    }
}