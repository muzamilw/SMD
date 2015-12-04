using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;

namespace SMD.MIS.ModelMappers
{
    public static class ProfileQuestionGroupMapper
    {
        /// <summary>
        /// Create From Dropdown
        /// </summary>
        public static ProfileQuestionGroupDropdown CreateFrom(this ProfileQuestionGroup source)
        {
            return new ProfileQuestionGroupDropdown
            {
                ProfileGroupId = source.ProfileGroupId,
                ProfileGroupName = source.ProfileGroupName
            };
        }
    }
}