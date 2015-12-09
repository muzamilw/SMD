using System.Configuration;
using System.Web;
using SMD.MIS.Areas.Api.Models;

namespace SMD.MIS.ModelMappers
{
    public static class ProfileQuestionAnswerMapper
    {
        /// <summary>
        /// Domain To Web Model 
        /// </summary>
        public static ProfileQuestionAnswer CreateFrom(this Models.DomainModels.ProfileQuestionAnswer source)
        {
            string path = source.ImagePath;
            if (!source.ImagePath.Contains("http"))
            {
                 path = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.ImagePath;
            }

            return new ProfileQuestionAnswer
            {
                PqId = source.PqId,
                Type = source.Type,
                AnswerString = source.AnswerString,
                ImagePath = path,

                LinkedQuestion1Id = source.LinkedQuestion1Id,
                LinkedQuestion2Id = source.LinkedQuestion2Id,
                LinkedQuestion3Id = source.LinkedQuestion3Id,
                LinkedQuestion4Id = source.LinkedQuestion4Id,

                LinkedQuestion5Id = source.LinkedQuestion5Id,
                LinkedQuestion6Id = source.LinkedQuestion6Id,
                PqAnswerId = source.PqAnswerId,
                SortOrder = source.SortOrder
            };
        }



        /// <summary>
        /// Web To Domain Model 
        /// </summary>
        public static Models.DomainModels.ProfileQuestionAnswer CreateFrom(this ProfileQuestionAnswer source)
        {
            return new Models.DomainModels.ProfileQuestionAnswer
            {
                PqId = source.PqId,
                Type = source.Type,
                AnswerString = source.AnswerString,
                ImagePath = source.ImagePath,

                LinkedQuestion1Id = source.LinkedQuestion1Id,
                LinkedQuestion2Id = source.LinkedQuestion2Id,
                LinkedQuestion3Id = source.LinkedQuestion3Id,
                LinkedQuestion4Id = source.LinkedQuestion4Id,

                LinkedQuestion5Id = source.LinkedQuestion5Id,
                LinkedQuestion6Id = source.LinkedQuestion6Id,
                PqAnswerId = source.PqAnswerId,
                SortOrder = source.SortOrder
            };
        }
    }
}