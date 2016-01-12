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
        public static Industry CreateFrom(this Industry source)
        {
            return new Industry
            {
                IndustryId = source.IndustryId,
                IndustryName = source.IndustryName
            };
        }

        public static IndusteryDropdown CreateForDd(this Industry source )
        {
            return new IndusteryDropdown
            {
                IndusteryId = source.IndustryId,
                IndusteryName = source.IndustryName
            };
        }
    }
}