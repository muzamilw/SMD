
using System.Collections.Generic;
using SMD.Models.ResponseModels;

namespace SMD.MIS.Areas.Api.Models
{
    public class IndusteryDropdown
    {
        public long IndusteryId { get; set; }
        public string IndusteryName { get; set; }
    }
    public class Industry
    {
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
    }

    public class IndustryResponse : BaseApiResponse
    {
        public IEnumerable<Industry> Industries { get; set; }
    }
}