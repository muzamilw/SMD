﻿using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{
    public class SurveyQuestionRepository : BaseRepository<SurveyQuestion>, ISurveyQuestionRepository
    {

        /// <summary>
        /// Survey Question Orderby clause
        /// </summary>
        private readonly Dictionary<SurveyQuestionByColumn, Func<SurveyQuestion, object>> _surveyQuestionOrderByClause = 
            new Dictionary<SurveyQuestionByColumn, Func<SurveyQuestion, object>>
                    {
                        {SurveyQuestionByColumn.Question, d => d.Question}  ,    
                        {SurveyQuestionByColumn.Description, d => d.Description} ,     
                        {SurveyQuestionByColumn.DisplayQuestion, d => d.DisplayQuestion}  ,    
                        {SurveyQuestionByColumn.CreatiedBy, d => d.User.FullName} ,     
                        {SurveyQuestionByColumn.SubmissionDate, d => d.SubmissionDate}      
                    };
        public SurveyQuestionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SurveyQuestion> DbSet
        {
            get
            {
                return db.SurveyQuestions;
            }
        }

        public SurveyQuestion Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SurveyQuestion> SearchSurveyQuestions(SurveySearchRequest request, out int rowCount)
        {
            if (request == null)
            {
                int fromRow = 0;
                int toRow = 10;
                rowCount = DbSet.Count();
                return DbSet.Where(g=>g.UserId == LoggedInUserIdentity).OrderBy(g => g.SqId)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
            }
            else
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                Expression<Func<SurveyQuestion, bool>> query =
                    question =>
                        (string.IsNullOrEmpty(request.SearchText) ||
                         (question.Question.Contains(request.SearchText)))
                         && (request.CountryFilter == 0 ||  question.CountryId == request.CountryFilter) 
                         &&( question.UserId == LoggedInUserIdentity)
                         && (request.LanguageFilter == 0 || question.LanguageId == request.LanguageFilter);


                rowCount = DbSet.Count(query);
                return DbSet.Where(query).OrderBy(g=>g.SqId)
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
            }
        }

        /// <summary>
        /// Get Rejected Survey Questions | baqer
        /// </summary>
        public IEnumerable<SurveyQuestion> SearchRejectedProfileQuestions(SurveySearchRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<SurveyQuestion, bool>> query =
                question => question.Status == (Int32)AdCampaignStatus.SubmitForApproval;

            rowCount = DbSet.Count(query);
            return request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(_surveyQuestionOrderByClause[request.SurveyQuestionOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(_surveyQuestionOrderByClause[request.SurveyQuestionOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
        }

        /// <summary>
        /// Get All survey Questions
        /// </summary>
        public IEnumerable<SurveyQuestion> GetAll()
        {
            return DbSet.Select(survey => survey).ToList();
        }
        public SurveyQuestion Get(long SqId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            return DbSet.Where(survey => survey.SqId == SqId).Include("SurveyQuestionTargetCriterias").Include("SurveyQuestionTargetLocations").SingleOrDefault();
        }

        /// <summary>
        /// Get Ads Campaigns | SP-API | baqer
        /// </summary>
        public IEnumerable<GetAds_Result> GetAdCompaignForApi(GetAdsApiRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            return db.GetAdCompaignForApi(request.UserId, fromRow, toRow);
        }

        /// <summary>
        /// Get Surveys | SP-API | baqer
        /// </summary>
        public IEnumerable<GetSurveys_Result> GetSurveysForApi(GetSurveysApiRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            return db.GetSurveysForApi(request.UserId, fromRow, toRow);  
        }

        /// <summary>
        /// Returns count of Matches Surveys| baqer
        /// </summary>
        public long GetAudienceSurveyCount(GetAudienceSurveyRequest request)
        {
           var resposne=  db.GetAudienceSurveyCount(request).ToList();
           return resposne.FirstOrDefault();
        }

        /// <summary>
        /// Returns count of Matches AdCampaigns | baqer
        /// </summary>
        public long GetAudienceAdCampaignCount(GetAudienceSurveyRequest request)
        {
            var resposne = db.GetAudienceAdCampaignCount(request);
            return resposne.FirstOrDefault(); 
        }

        public GetAudience_Result GetAudienceCount(GetAudienceCountRequest request)
        {
            return db.GetAudienceCampaignAndSurveyCounts(request.ageFrom, request.ageTo, request.gender, request.countryIds, request.cityIds, request.languageIds, request.industryIds
                , request.profileQuestionIds, request.profileAnswerIds, request.surveyQuestionIds, request.surveyAnswerIds,
                request.countryIdsExcluded, request.cityIdsExcluded, request.languageIdsExcluded, request.industryIdsExcluded).FirstOrDefault();
        }
        public UserBaseData getBaseData()
        {
            UserBaseData data = new UserBaseData();
            var usr = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();
            if(usr!= null)
            {
                
                var country = 
                 data.CityId = usr.CityId;
                 data.CountryId = usr.CountryId;
                 data.EducationId = usr.EducationId;
                 data.IndustryId = usr.IndustryId;
                 data.LanguageId = usr.LanguageId;
                 data.City = usr.City != null ? usr.City.CityName : "";
                 data.Country = usr.Country != null ? usr.Country.CountryName : "";
                 data.Education = usr.Education != null?usr.Education.Title : "";
                 data.Industry = usr.Industry != null?usr.Industry.IndustryName:"";
                 data.Language = usr.Language != null? usr.Language.LanguageName: "";
                 data.isStripeIntegrated = String.IsNullOrEmpty(usr.StripeCustomerId) == true ? false : true;
                 data.Latitude = usr.City != null ? usr.City.GeoLat : "";
                 data.Longitude = usr.City != null ? usr.City.GeoLong : "";
            }

            return data;
        }
    }
}
