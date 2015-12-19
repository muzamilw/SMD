
using System.Collections.Generic;

namespace SMD.Models.RequestModels
{
    /// <summary>
    /// Get Audience Survey Request | Returns Count of Users 
    /// </summary>
    public class GetAudienceSurveyRequest
    {
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public long LanguageId { get; set; }
        public long IndustryId { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public string IdsList { get; set; }
        public List<long> ProfileQuestionIds { get; set; }
    }
}
