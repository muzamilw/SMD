using System.Collections.Generic;
using System.Linq;
using SMD.MIS.Areas.Api.Models;

namespace SMD.MIS.ModelMappers
{

    public static class EducationMapper
    {
        public static Education CreateFrom(this Models.DomainModels.Education source)
        {
            return new Education
            {
                EducationId = source.EducationId,
                Title = source.Title
            };
        }

        public static EducationDropdown CreateFromDd(this Models.DomainModels.Education source)
        {
            return new EducationDropdown
            {
                EducationId = source.EducationId,
                EducationName = source.Title
            };
        }

        public static EducationResponse CreateFrom(this Models.ResponseModels.EducationResponse source)
        {
            return new EducationResponse
            {
                Educations = source.Educations != null ? source.Educations.Select(edu => edu.CreateFrom()) : new List<Education>(),
                Status = source.Status,
                Message = source.Message
            };
        }

    }
}