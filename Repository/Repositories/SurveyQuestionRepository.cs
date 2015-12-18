using Microsoft.Practices.Unity;
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
        // <summary>
        /// update survey images
        /// </summary>
        public bool updateSurveyImages(string[] imagePathsList,long surveyID)
        {
            SurveyQuestion survey = DbSet.Where(g => g.SqId == surveyID).SingleOrDefault();
            if(survey != null)
            {
                survey.LeftPicturePath = imagePathsList[0];
                survey.RightPicturePath = imagePathsList[1];
                SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get Ads Campaigns | SP-API | baqer
        /// </summary>
        public IEnumerable<GetAds_Result> GetAdCompaignForApi(string userId)
        {
            return db.GetAdCompaignForApi(userId,0,0).ToList();
        }

    }
}
