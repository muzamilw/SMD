using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{

    public static class EducationMapper
    {
        public static SMD.MIS.Areas.Api.Models.Education CreateFrom(this SMD.Models.DomainModels.Education source)
        {
            return new SMD.MIS.Areas.Api.Models.Education
            {
                EducationId = source.EducationId,
                Title = source.Title
            };
        }

        public static EducationDropdown CreateFromDd(this SMD.Models.DomainModels.Education source)
        {
            return new EducationDropdown
            {
                EducationId = source.EducationId,
                EducationName = source.Title
            };
        }
    }
}