using System.Collections.Generic;
using SMD.Models.DomainModels;

namespace SMD.Models.ResponseModels
{
    /// <summary>
    /// Education Response 
    /// </summary>
    public class EducationResponse : BaseApiResponse
    {
       public IEnumerable<Education> Educations { get; set; }
    }
}
