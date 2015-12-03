using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.Common;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Profile Question Repository 
    /// </summary>
    public class ProfileQuestionRepository : BaseRepository<ProfileQuestion>, IProfileQuestionRepository
    {
        #region Private
        /// <summary>
        /// Profile Question Orderby clause
        /// </summary>
        private readonly Dictionary<ProfileQuestionByColumn, Func<ProfileQuestion, object>> _profileQuestionOrderByClause = new Dictionary<ProfileQuestionByColumn, Func<ProfileQuestion, object>>
                    {
                        {ProfileQuestionByColumn.Question, d => d.Question}      ,
                        {ProfileQuestionByColumn.Group, d => d.ProfileQuestionGroup},
                        {ProfileQuestionByColumn.HasLinked, d => d.HasLinkedQuestions},
                        {ProfileQuestionByColumn.Priority, d => d.Priority}    
                    };
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ProfileQuestionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProfileQuestion> DbSet
        {
            get { return db.ProfileQuestions; }
        }
        #endregion
        #region Public
      
        /// <summary>
        /// Find Queston by Id 
        /// </summary>
        public ProfileQuestion Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Profile Question Repository Interface 
        /// </summary>
        public IEnumerable<ProfileQuestion> SearchProfileQuestions(ProfileQuestionSearchRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<ProfileQuestion, bool>> query =
                question =>
                    (string.IsNullOrEmpty(request.ProfileQuestionFilterText) ||
                     (question.Question.Contains(request.ProfileQuestionFilterText)))
                     &&
                     (question.ProfileQuestionGroup.ProfileGroupId==request.QuestionGroupFilter)
                     &&
                     (question.CountryId==request.CountryFilter)
                     &&
                     (question.LanguageId==request.LanguageFilter)
                     && (question.Status == null || question.Status == 1);   // 0 -> archived  || 1 -> active

            rowCount = DbSet.Count(query);
            return request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(_profileQuestionOrderByClause[request.ProfileQuestionOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(_profileQuestionOrderByClause[request.ProfileQuestionOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList(); 
        }

        #endregion
    }
}
