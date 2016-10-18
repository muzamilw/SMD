using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
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
                        {ProfileQuestionByColumn.Group, d => d.ProfileQuestionGroup.ProfileGroupId},
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
           // ProfileQuestionTargetLocation

            return DbSet.Include("ProfileQuestionTargetLocations.City").Include("ProfileQuestionTargetLocations.Country").Where(i => i.PqId == id).FirstOrDefault();
            
           // Query.ProfileQuestionTargetLocations.include<>
        }

        /// <summary>
        /// Profile Question Repository Interface 
        /// </summary>
        public IEnumerable<ProfileQuestion> SearchProfileQuestions(ProfileQuestionSearchRequest request, out int rowCount)
        {
            db.Configuration.LazyLoadingEnabled = true;
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<ProfileQuestion, bool>> query = null;
            if ( request.fmode == true)
            {
                 query =
                question =>
                    (string.IsNullOrEmpty(request.ProfileQuestionFilterText) ||
                     (question.Question.Contains(request.ProfileQuestionFilterText)))
                     &&
                     (request.QuestionGroupFilter == 0 || question.ProfileQuestionGroup.ProfileGroupId == request.QuestionGroupFilter)
                     &&
                     (question.CountryId == request.CountryFilter)
                     &&
                     (question.LanguageId == request.LanguageFilter)
                     && (question.CompanyId == null)
                     && question.Status != 7;   
            }
            else
            {
                 query =
                question =>
                    (string.IsNullOrEmpty(request.ProfileQuestionFilterText) ||
                     (question.Question.Contains(request.ProfileQuestionFilterText)))
                     &&
                     (request.QuestionGroupFilter == 0 || question.ProfileQuestionGroup.ProfileGroupId == request.QuestionGroupFilter)
                     &&
                     (question.CountryId == request.CountryFilter)
                     &&
                     (question.LanguageId == request.LanguageFilter)
                     && (question.CompanyId == this.CompanyId)
                     && question.Status != 7;   
            }


            rowCount = DbSet.Count(query);
           // DbSet.Where(query).Include(a => a.ProfileQuestionTargetCriterias);
             DbSet.Where(query).Include(a => a.ProfileQuestionTargetLocations).Include("City");

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

        /// <summary>
        /// Get All Profile Questions
        /// </summary>
        public IEnumerable<ProfileQuestion> GetAllProfileQuestions()
        {
            return DbSet.Where(question => question.Status == null || question.Status == 1).ToList();
        }


        /// <summary>
        /// Get Count of PQ For Group Id
        /// </summary>
        public int GetTotalCountOfGroupQuestion(double groupId)
        {
            return DbSet.Count(question => question.ProfileGroupId == groupId);
        }

        /// <summary>
        /// Get Answered Questions count 
        /// </summary>
        public int GetCountOfUnAnsweredQuestionsByGroupId(double groupId,string userId)
        {
            var countOfUnAnsweredQuestions = (from question in db.ProfileQuestions
                                              where (question.ProfileGroupId == groupId &&
                                              !(db.ProfileQuestionUserAnswers.Distinct().Any(ans => ans.PqId == question.PqId && ans.UserId == userId)))
                                              select question.PqId).ToList().Count;
            return countOfUnAnsweredQuestions;
        }


        /// <summary>
        /// Get Un-answered Profile Questions by Group Id 
        /// </summary>
        public IEnumerable<ProfileQuestion> GetUnansweredQuestionsByGroupId(GetProfileQuestionApiRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            var unAnsweredQuestions = (from question in db.ProfileQuestions
                     where (question.ProfileGroupId==request.GroupId && !(db.ProfileQuestionUserAnswers.Any(ans => ans.PqId == question.PqId)))
                select question)
                .OrderBy(qst => qst.ProfileQuestionGroup.ProfileGroupName)
                 .Skip(fromRow)
                 .Take(toRow)
                 .ToList();
            return unAnsweredQuestions;
        }

        public IEnumerable<ProfileQuestion> UpdateQuestionsCompanyID(IEnumerable<ProfileQuestion> ProfileQuestions)
        {
           ///////Setting CompanyId against question if he is user else setting null means these question related to admin
            if(this.CompanyId>0)
            {
                foreach (var Question in ProfileQuestions)
                {
                    if (Question.CompanyId == null || Question.CompanyId == 0)
                    {
                        Question.CompanyId = this.CompanyId;

                        db.ProfileQuestions.Attach(Question);

                        db.Entry(Question).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            return ProfileQuestions;
        }
        public void AddProfileQuestions(ProfileQuestion Obj)
        {
            Obj.CompanyId = this.CompanyId;
            DbSet.Add(Obj);
        }
        public UserBaseData getBaseData()
        {
            UserBaseData data = new UserBaseData();
            var usr = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();

            if (usr != null)
            {
                bool isAdmin = false;


                
                data.CountryId = usr.Company == null ? null : usr.Company.CountryId;
                data.EducationId = usr.EducationId;
                data.IndustryId = usr.IndustryId;
                data.LanguageId = usr.LanguageId;
                data.City = usr.Company.City != null ? usr.Company.City : "";
                data.Country = usr.Company.Country != null ? usr.Company.Country.CountryName : "";
                data.Education = usr.Education != null ? usr.Education.Title : "";
                data.Industry = usr.Industry != null ? usr.Industry.IndustryName : "";
                data.Language = usr.Language != null ? usr.Language.LanguageName : "";
                data.isStripeIntegrated = usr.Company == null ? false : String.IsNullOrEmpty(usr.Company.StripeCustomerId) == true ? false : true;
                data.IsSpecialAccount = usr.Company.IsSpecialAccount;
                //data.Latitude = usr.Company.City != null ? usr.Company.City.GeoLat : "";
                //data.Longitude = usr.Company.City != null ? usr.Company.City.GeoLong : "";
                data.isUserAdmin = isAdmin;
            }

            return data;
        }

        public IEnumerable<ProfileQuestion> GetProfileQuestionsForApproval(GetPagedListRequest request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<ProfileQuestion, bool>> query =
                c => c.Status == (Int32)AdCampaignStatus.SubmitForApproval;
                rowCount = DbSet.Count(query);
                var res = DbSet.Where(query)
               .OrderByDescending(p => p.SubmissionDateTime);
                return res.Skip(fromRow)
                    .Take(toRow);

        }
        public IEnumerable<getSurvayByPQID_Result> getSurvayByPQIDAnalytics(int PQId, int CampStatus, int dateRange, int Granularity)
        {
          
            return db.getSurvayByPQIDAnalytics(PQId, CampStatus, dateRange, Granularity);
        }

        //public ProfileQuestion GetProfileQuestionByID(long PQID)
        //{ 
        
        //   return DbSet.Where(i=>i.PqId==PQID).Include<q=>Queryable.>

        //}

        #endregion
    }
}
