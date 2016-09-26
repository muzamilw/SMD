using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                SortOrder = source.SortOrder,
                ProfileQuestion = source.ProfileQuestion.CreateForProfileQuestion()

            };
        }

        public static ProfileQuestion CreateForProfileQuestion(this Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestion
            {
                PqId = source.PqId,
                Question = source.Question,
                Priority = source.Priority,
                HasLinkedQuestions = source.HasLinkedQuestions,
                ProfileGroupName = source.ProfileQuestionGroup != null ? source.ProfileQuestionGroup.ProfileGroupName : string.Empty,

                LanguageId = source.LanguageId,
                CountryId = source.CountryId,
                ProfileGroupId = source.ProfileGroupId,
                Type = source.Type,
                RefreshTime = source.RefreshTime,
                SkippedCount = source.SkippedCount,
                CreationDate = source.CreationDate,
                ModifiedDate = source.ModifiedDate,
                PenalityForNotAnswering = source.PenalityForNotAnswering,
                Status = source.Status,
                AnswerNeeded = source.AnswerNeeded,
                AsnswerCount = source.AsnswerCount,
                AgeRangeStart = source.AgeRangeStart,
                AgeRangeEnd = source.AgeRangeEnd,
                Gender = source.Gender,
                ProfileQuestionTargetLocation = source.ProfileQuestionTargetLocations != null ? source.ProfileQuestionTargetLocations.Select(loc => loc.CreateForRecieveTLocationPqAns()).ToList() : null,
                ProfileQuestionTargetCriteria = source.ProfileQuestionTargetCriterias1 != null ? source.ProfileQuestionTargetCriterias1.Select(crt => crt.CreateForRecieveTCriteriaPqAns()).ToList() : null
                
            };
        }

        public static SMD.MIS.Areas.Api.Models.ProfileQuestionTargetCriteria CreateForRecieveTCriteriaPqAns(this Models.DomainModels.ProfileQuestionTargetCriteria source)
        {
            string QuestionString = "";
            string AnswerString = "";
            string LanguageName = "";
            string IndustryName = "";
            string EducationName = "";
            string PQQuestionString = "";
            if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.ProfileQuestion)
            {

                if (source.PQQuestionID != null && source.PQQuestionID > 0)
                {
                    QuestionString = source.PQQuestionString;
                   // PQQuestionString = source.PQQuestionString;
                }
                if (source.PQAnswerID != null && source.PQAnswerID > 0 && source.ProfileQuestionAnswer != null)
                {
                    AnswerString = source.ProfileQuestionAnswer.AnswerString;
                }
            }
            //else if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.SurveyQuestion)
            //{
            //    if (source.SQID != null && source.SQID > 0 && source.SurveyQuestion != null)
            //    {
            //        QuestionString = source.SurveyQuestion.Question;
            //    }
            //    if (source.SqAnswer != null && source.SqAnswer > 0 && source.SurveyQuestion != null)
            //    {
            //        if (source.SqAnswer == 1)
            //        {
            //            if (!source.SurveyQuestion.LeftPicturePath.Contains("http:"))
            //            {
            //                AnswerString = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.SurveyQuestion.LeftPicturePath;
            //            }
            //            else
            //            {
            //                AnswerString = source.SurveyQuestion.LeftPicturePath;
            //            }

            //        }
            //        else
            //        {
            //            if (!source.SurveyQuestion.RightPicturePath.Contains("http:"))
            //            {
            //                AnswerString = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + source.SurveyQuestion.RightPicturePath;
            //            }
            //            else
            //            {
            //                AnswerString = source.SurveyQuestion.RightPicturePath;
            //            }

            //        }

            //    }
            //}
            else if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.Language)
            {
                if (source.LanguageID != null && source.LanguageID > 0 && source.Language != null)
                {
                    LanguageName = source.Language.LanguageName;
                }
            }
            else if (source.Type == (int)SurveyQuestionTargetCriteriaType.Industry)
            {
                if (source.Industry != null)
                {
                    IndustryName = source.Industry.IndustryName;
                }
            }
            else if (source.Type == (int)SurveyQuestionTargetCriteriaType.Education)
            {
                if (source.Education != null)
                {
                    EducationName = source.Education.Title;
                }
            }
            else if (source.Type == (int)AdCampaignCriteriaType.QuizQustion)
            {
                //if (source.QuizCampaign != null)
                //{
                //    QuestionString = source.QuizCampaign.VerifyQuestion;
                //    if (source.QuizAnswerId == 1)
                //        AnswerString = source.QuizCampaign.Answer1;
                //    if (source.QuizAnswerId == 2)
                //        AnswerString = source.QuizCampaign.Answer2;
                //}
                if (source.AdCampaignID != null && source.AdCampaignID > 0)
                {
                    QuestionString = source.PQQuestionString;
                }
                if (source.AdCampaignAnswer != null && source.AdCampaignAnswer > 0)
                {
                    AnswerString = source.AdCampaignAnswerString;
                }
            }
            return new SMD.MIS.Areas.Api.Models.ProfileQuestionTargetCriteria
            {


                IncludeorExclude = source.IncludeorExclude,
                IndustryID = source.IndustryID,
                LanguageID = source.LanguageID,
                PQAnswerID = source.PQAnswerID,
                PQQuestionID = source.PQQuestionID,
                PQID = source.PQID,
                //SQAnswer = source.SqAnswer,
                SQID = source.SQID,
                Type = source.Type,
                questionString = QuestionString,
                answerString = AnswerString,
                Language = LanguageName,
                Industry = IndustryName,
                EducationID = source.EducationID,
                Education = EducationName,
                AdCampaignAnswer = source.AdCampaignAnswer,
                AdCampaignID = source.AdCampaignID,
                PQQuestionString=PQQuestionString,
                IsDeleted=source.IsDeleted,
                ID=source.ID
            };
        }

        /// <summary>
        /// Domain to Web Mapper
        /// </summary>
        public static SMD.MIS.Areas.Api.Models.ProfileQuestionTargetLocation CreateForRecieveTLocationPqAns(this Models.DomainModels.ProfileQuestionTargetLocation source)
        {
            string CName = "";
            string CountName = "";
            string latitdue = "";
            string longitude = "";
            if (source.CountryID != null && source.CountryID > 0 && source.Country != null)
            {
                CountName = source.Country.CountryName;

            }
            if (source.CityID != null && source.CityID > 0 && source.City != null)
            {
                CName = source.City.CityName;
                latitdue = source.City.GeoLAT;
                longitude = source.City.GeoLONG;
            }
            return new SMD.MIS.Areas.Api.Models.ProfileQuestionTargetLocation
            {

                ID = source.ID,
                CityID = source.City.CityId,
                CountryID = source.Country.CountryId,
                IncludeorExclude = source.IncludeorExclude,
                Radius = source.Radius,
                City = CName,
                Country = CountName,
                Latitude = latitdue,
                Longitude = longitude,
                PQID = source.PQID,
               
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