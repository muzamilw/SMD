using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.ModelMappers
{

    public static class IndustryMapper
    {
        public static SMD.MIS.Areas.Api.Models.Industry CreateFrom(this SMD.Models.DomainModels.Industry source)
        {
            return new SMD.MIS.Areas.Api.Models.Industry
            {
                IndustryId = source.IndustryId,
                IndustryName = source.IndustryName
            };
        }

        public static IndusteryDropdown CreateForDd(this SMD.Models.DomainModels.Industry source )
        {
            return new IndusteryDropdown
            {
                IndusteryId = source.IndustryId,
                IndusteryName = source.IndustryName
            };
        }
    }
}