using SMD.MIS.Areas.Api.Models;
using SMD.Models.DomainModels;

namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Language Mapper
    /// </summary>
    public static class LanguageMapper
    {
        public static LanguageDropdown CreateFrom(this Language source)
        {
            return new LanguageDropdown
            {
                LanguageId = source.LanguageId,
                LanguageName = source.LanguageName
            };
        }
    }
}