using SMD.MIS.Areas.Api.Models;
using SMD.Models.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using ProfileQuestionAnswer = SMD.Models.DomainModels.ProfileQuestionAnswer;
using ProfileQuestionTargetCriteriaDM = SMD.Models.DomainModels.ProfileQuestionTargetCriteria;
using ProfileQuestionTargetLocationDM = SMD.Models.DomainModels.ProfileQuestionTargetLocation;
namespace SMD.MIS.ModelMappers
{
    /// <summary>
    /// Profile Question Mapper
    /// </summary>
    public static class ProfileQuestionMapper
    {
        #region MIS
        /// <summary>
        /// Domain Search Response to Web Response 
        /// </summary>
        public static ProfileQuestionSearchRequestResponse CraeteFrom(
            this Models.ResponseModels.ProfileQuestionSearchRequestResponse source)
        {
            return new ProfileQuestionSearchRequestResponse
            {
                TotalCount = source.TotalCount,
                Professions = source.Professions.Select(profession => profession.CreateFrom()),
                ProfileQuestions = source.ProfileQuestions.Select(question => question.CreateFrom())
            };
        }

        /// <summary>
        /// Domain To Web 
        /// </summary>
        public static ProfileQuestion CreateFrom(this Models.DomainModels.ProfileQuestion source)
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
                AnswerNeeded=source.AnswerNeeded,
                AsnswerCount=source.AsnswerCount,
                AgeRangeStart=source.AgeRangeStart,
                AgeRangeEnd=source.AgeRangeEnd,
                Gender=source.Gender,
                ProfileQuestionTargetLocation =source.ProfileQuestionTargetLocations!=null?source.ProfileQuestionTargetLocations.Select(loc => loc.CreateForRecieveTLocation()).ToList():null,
                ProfileQuestionTargetCriteria = source.ProfileQuestionTargetCriterias1!=null? source.ProfileQuestionTargetCriterias1.Select(crt => crt.CreateForRecieveTCriteria()).ToList():null

            };
        }

        /// <summary>
        /// Web to Domain 
        /// </summary>
        public static Models.DomainModels.ProfileQuestion CreateFrom(this ProfileQuestion source)
        {
            return new Models.DomainModels.ProfileQuestion
            {
                PqId = source.PqId,
                Question = source.Question,
                Priority = source.Priority,
                HasLinkedQuestions = source.HasLinkedQuestions,
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
                AgeRangeStart=source.AgeRangeStart,
                AgeRangeEnd=source.AgeRangeEnd,
                Gender=source.Gender,
                ProfileQuestionTargetCriterias = source.ProfileQuestionTargetCriteria != null ? source.ProfileQuestionTargetCriteria.Select(crt => crt.CreateFromTargetCriteria()).ToList() : new Collection<ProfileQuestionTargetCriteriaDM>().ToList(),
                ProfileQuestionTargetLocations = source.ProfileQuestionTargetLocation != null ? source.ProfileQuestionTargetLocation.Select(loc => loc.CreateFromTargetLocation()).ToList() : new Collection<ProfileQuestionTargetLocationDM>().ToList(),
                ProfileQuestionAnswers = source.ProfileQuestionAnswers != null ? source.ProfileQuestionAnswers.Select(ans => ans.CreateFrom()).ToList() : new Collection<ProfileQuestionAnswer>().ToList()
            };
        }
        /// <summary>
        /// Create DD from Domain Model 
        /// </summary>
        public static ProfileQuestionDropdown CreateFromDropdown(this Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestionDropdown
            {
                PqId = source.PqId,
                Question = source.Question
            };
        }

        /// <summary>
        /// Creates web model of BaseData
        /// </summary>
        public static ProfileQuestionBaseResponse CreateFrom(
            this Models.ResponseModels.ProfileQuestionBaseResponse source)
        {
            return new ProfileQuestionBaseResponse
            {
                CountryDropdowns = source.Countries.Select(country => country.CreateFrom()),
                LanguageDropdowns = source.Languages.Select(lang => lang.CreateFrom()),
                ProfileQuestionGroupDropdowns = source.ProfileQuestionGroups.Select(group => group.CreateFrom()),
                ProfileQuestionDropdowns = source.ProfileQuestions.Select(question => question.CreateFromDropdown()),
                objBaseData=source.objBaseData.CreateFromBaseData()
            };
        }

        #endregion
        #region APIs

        /// <summary>
        /// Domain to Api
        /// </summary>
        public static ProfileQuestionApiModel CreateForApi(this Models.DomainModels.ProfileQuestion source)
        {
            return new ProfileQuestionApiModel
            {
                PqId= source.PqId,
                Question = source.Question,
                QuestionType= source.Type,
                ProfileGroupName= source.ProfileQuestionGroup.ProfileGroupName,
                ProfileQuestionAnswers = source.ProfileQuestionAnswers.Select(ans => ans.CreateForApi()).ToList()
            };
        }

        /// <summary>
        /// Response to Apis Call
        /// </summary>
        public static ProfileQuestionApiSearchResponse CreateFrom(this Models.ResponseModels.ProfileQuestionApiSearchResponse source)
        {
            return new ProfileQuestionApiSearchResponse
            {
                ProfileQuestionApiModels = source.ProfileQuestions.Select(question => question.CreateForApi()),
                PercentageCompleted= source.PercentageCompleted
            };
        }
          public static SMD.MIS.Areas.Api.Models.UserBaseData CreateFromBaseData(this Models.Common.UserBaseData source)
          {
            if (source != null)
            {
                return new SMD.MIS.Areas.Api.Models.UserBaseData
                {
                    CityId = source.CityId,
                    CountryId = source.CountryId,
                    LanguageId = source.LanguageId,
                    IndustryId = source.IndustryId,
                    EducationId = source.EducationId,
                    City = source.City,
                    Country = source.Country,
                    Language = source.Language,
                    Industry = source.Industry,
                    Education = source.Education,
                    CurrencySymbol = source.CurrencySymbol,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude,
                    isStripeIntegrated = source.isStripeIntegrated,
                    isUserAddmin = source.isUserAdmin
                };
            }
            else
            {
                return null;
            }
        }

          public static Models.DomainModels.ProfileQuestionTargetCriteria CreateFromTargetCriteria(this ProfileQuestionTargetCriteria source)
          {
              if (source != null)
              {
                  return  new Models.DomainModels.ProfileQuestionTargetCriteria
                  { 
                      ID=source.ID,
                      PQID  = source.PQID,
                      Type = source.Type,
                      SQID = source.SQID,
                      PQAnswerID = source.PQAnswerID,
                      LinkedSQID = source.LinkedSQID,
                      LinkedSQAnswer = source.LinkedSQAnswer,
                      IncludeorExclude = source.IncludeorExclude,
                      LanguageID = source.LanguageID,
                      IndustryID = source.IndustryID,
                      EducationID = source.EducationID,
                      AdCampaignAnswer=source.AdCampaignAnswer,
                      PQQuestionID=source.PQQuestionID,
                      AdCampaignID=source.AdCampaignID,
                      IsDeleted=source.IsDeleted
                  };
              }
              else
              {
                  return null;
              }
          }
          public static Models.DomainModels.ProfileQuestionTargetLocation CreateFromTargetLocation(this ProfileQuestionTargetLocation source)
          {
              if (source != null)
              {
                  return new Models.DomainModels.ProfileQuestionTargetLocation
                  {
                      ID = source.ID,
                      PQID = source.PQID,
                      CountryID = source.CountryID,
                      CityID = source.CityID,
                      Radius = source.Radius,
                      IncludeorExclude = source.IncludeorExclude,
                      IsDeleted=source.IsDeleted
                  };
              }
              else
              {
                  return null;
              }
          }


          public static SMD.MIS.Areas.Api.Models.ProfileQuestionTargetCriteria CreateForRecieveTCriteria(this Models.DomainModels.ProfileQuestionTargetCriteria source)
          {
              string QuestionString = "";
              string AnswerString = "";
              string LanguageName = "";
              string IndustryName = "";
              string EducationName = "";
              

              if (source.Type != null && source.Type == (int)AdCampaignCriteriaType.ProfileQuestion)
              {
                  if (source.PQQuestionID != null && source.PQQuestionID > 0 && source.ProfileQuestion != null)
                  {
                      QuestionString = source.ProfileQuestion.Question;
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
              //else if (source.Type == (int)AdCampaignCriteriaType.QuizQustion)
              //{
              //    if (source.QuizCampaign != null)
              //    {
              //        QuestionString = source.QuizCampaign.VerifyQuestion;
              //        if (source.QuizAnswerId == 1)
              //            AnswerString = source.QuizCampaign.Answer1;
              //        if (source.QuizAnswerId == 2)
              //            AnswerString = source.QuizCampaign.Answer2;
              //    }
              //}
              return new SMD.MIS.Areas.Api.Models.ProfileQuestionTargetCriteria
              {


                  IncludeorExclude = source.IncludeorExclude,
                  IndustryID = source.IndustryID,
                  LanguageID = source.LanguageID,
                  PQAnswerID = source.PQAnswerID,
                  PQQuestionID=source.PQQuestionID,
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
                  AdCampaignID = source.AdCampaignID
              };
          }

          /// <summary>
          /// Domain to Web Mapper
          /// </summary>
          public static SMD.MIS.Areas.Api.Models.ProfileQuestionTargetLocation CreateForRecieveTLocation(this Models.DomainModels.ProfileQuestionTargetLocation source)
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
                  latitdue = source.City.GeoLat;
                  longitude = source.City.GeoLong;
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
                  PQID=source.PQID,
                  IsDeleted=source.IsDeleted
              };
          }
        /// <summary>
        /// Domain to API response 
        /// </summary>
        public static UpdateProfileQuestionUserAnswerResponse CreateFrom(this string source)
        {
            return new UpdateProfileQuestionUserAnswerResponse
            {
                Response = source
            };
        }
        #endregion 
    }
}