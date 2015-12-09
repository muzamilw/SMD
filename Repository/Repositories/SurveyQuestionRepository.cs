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
                        {SurveyQuestionByColumn.DisplayQuestion, d => d.DisplayQuestion}      
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
        /// Get Rejected Survey Questions
        /// </summary>
        public IEnumerable<SurveyQuestion> SearchRejectedProfileQuestions(SurveySearchRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<SurveyQuestion, bool>> query =
                question => question.Approved==false;

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
    }
}
