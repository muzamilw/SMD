
using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    public class EducationDropdown
    {
        /// <summary>
        /// Education id
        /// </summary>
        public long EducationId { get; set; }

        /// <summary>
        /// Education Title
        /// </summary>
        public string EducationName { get; set; }
    }
    public class Education
    {
        /// <summary>
        /// Education id
        /// </summary>
        public long EducationId { get; set; }

        /// <summary>
        /// Education Title
        /// </summary>
        public string Title { get; set; }
    }

    public class EducationResponse : BaseApiResponse
    {
        public IEnumerable<Education> Educations { get; set; }
    }
}