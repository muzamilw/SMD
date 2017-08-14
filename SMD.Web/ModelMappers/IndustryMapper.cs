using System.Collections.Generic;
using System.Linq;
using SMD.MIS.Areas.Api.Models;
using Industry = SMD.MIS.Areas.Api.Models.Industry;

namespace SMD.MIS.ModelMappers
{

    public static class IndustryMapper
    {
        public static Industry CreateFrom(this Models.DomainModels.Industry source)
        {
            return new Industry
            {
                IndustryId = source.IndustryId,
                IndustryName = source.IndustryName
            };
        }

        public static IndusteryDropdown CreateForDd(this Models.DomainModels.Industry source )
        {
            return new IndusteryDropdown
            {
                IndusteryId = source.IndustryId,
                IndusteryName = source.IndustryName
            };
        }

        public static IndustryResponse CreateFrom(this Models.ResponseModels.IndustryResponse source)
        {
            return new IndustryResponse
            {
                Industries = source.Industries != null ? source.Industries.Select(ind => ind.CreateFrom()) : new List<Industry>(),
                Status = true,
                Message = "Success"
            };
        }
    }
}