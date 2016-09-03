﻿using SMD.MIS.Areas.Api.Models;
using System.Web;

namespace SMD.MIS.ModelMappers
{
    public static class ProfileQuestionAnswerMapper
    {
        /// <summary>
        /// Domain To Web Model 
        /// </summary>
        public static ProfileQuestionAnswer CreateFrom(this Models.DomainModels.ProfileQuestionAnswer source)
        {

            return new ProfileQuestionAnswer
            {
                PqId = source.PqId,
                Type = source.Type,
                AnswerString = source.AnswerString,
                ImagePath = GetImagePath(source),

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

        /// <summary>
        /// Create DD from Domain Model 
        /// </summary>
        public static ProfileQuestionAnswerDropdown CreateFromDropdown(this Models.DomainModels.ProfileQuestionAnswer source)
        {
            return new ProfileQuestionAnswerDropdown
            {
                PqAnswerId = source.PqAnswerId,
                Answer = source.AnswerString
            };
        }

        /// <summary>
        /// Create Model For API
        /// </summary>
        public static ProfileQuestionAnswerView CreateFromForProduct(this Models.DomainModels.ProfileQuestionAnswer source)
        {
            return new ProfileQuestionAnswerView
            {
                PqAnswerId = source.PqAnswerId,
                Answer = source.AnswerString,
                ImagePath = source.ImagePath,
                PqId = source.PqId,
                Type = source.Type,
                SortOrder = source.SortOrder,
                LinkedQuestion1Id = source.LinkedQuestion1Id,
                LinkedQuestion2Id = source.LinkedQuestion2Id
            };
        }

        /// <summary>
        /// Create Model For API
        /// </summary>
        public static ProfileQuestionAnswerApiModel CreateForApi(this Models.DomainModels.ProfileQuestionAnswer source)
        {
            return new ProfileQuestionAnswerApiModel
            {
                PqAnswerId = source.PqAnswerId,
                AnswerString = source.AnswerString,
                ImagePath = GetImagePath(source),
                PqId = source.PqId,
                Type = source.Type
            };
        }

        /// <summary>
        /// Returns Image Path for Answer Image 
        /// </summary>
        private static string GetImagePath(Models.DomainModels.ProfileQuestionAnswer source)
        {
            string path = source.ImagePath;
            if (source.ImagePath != null && !source.ImagePath.Contains("http"))
            {
                path = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.ImagePath;
            }
            return path;
        }


    }
}