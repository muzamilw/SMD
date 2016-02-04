using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Industry Response 
    /// </summary>
    public class IndustryResponse : BaseApiResponse
    {
       public IEnumerable<Industry> Industries { get; set; }
    }
}
