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
                Question = source.Question
            };
        }
    }
}