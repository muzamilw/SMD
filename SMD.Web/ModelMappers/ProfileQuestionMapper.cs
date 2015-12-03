using System;
using SMD.MIS.Models.RequestResposeModels;
using SMD.MIS.Models.WebModels;
using System.Linq;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Profile Question Mapper
    /// </summary>
    public static class ProfileQuestionMapper
    {
        /// <summary>
        /// Domain Search Response to Web Response 
        /// </summary>
        public static ProfileQuestionSearchRequestResponse CraeteFrom(
            this SMD.Models.ResponseModels.ProfileQuestionSearchRequestResponse source)
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
        public static ProfileQuestion CreateFrom(this SMD.Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestion
            {
                PqId = source.PqId,
                Question = source.Question,
                Priority = source.Priority,
                HasLinkedQuestions = source.HasLinkedQuestions,
                ProfileGroupId = source.ProfileGroupId,
                ProfileGroupName = source.ProfileQuestionGroup.ProfileGroupName
            };
        }

        /// <summary>
        /// Web to Domain 
        /// </summary>
        public static SMD.Models.DomainModels.ProfileQuestion CreateFrom(this ProfileQuestion source)
        {
            return new SMD.Models.DomainModels.ProfileQuestion
            {
                PqId = source.PqId,
                Question = source.Question,
                Priority = source.Priority,
                HasLinkedQuestions = source.HasLinkedQuestions,
            };
        }

        /// <summary>
        /// Creates web model of BaseData
        /// </summary>
        public static ProfileQuestionBaseResponse CreateFrom(
            this SMD.Models.ResponseModels.ProfileQuestionBaseResponse source)
        {
            return new ProfileQuestionBaseResponse
            {
                CountryDropdowns = source.Countries.Select(country => country.CreateFrom()),
                LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom()),
                ProfileQuestionGroupDropdowns = source.ProfileQuestionGroups.Select(group => group.CreateFrom())
            };
        }


    }
}