using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;

namespace SMD.Repository.BaseRepository
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// 
    [Serializable]
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, long>
       where TDomainClass : class
    {
        #region Private

        // ReSharper disable once InconsistentNaming
        private readonly IUnityContainer container;
        #endregion
        #region Protected
        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract IDbSet<TDomainClass> DbSet { get; }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseRepository(IUnityContainer container)
        {

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;
            db = (BaseDbContext)container.Resolve(typeof(BaseDbContext), new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });
           
        }

        #endregion
        #region Public
        /// <summary>
        /// base Db Context
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public BaseDbContext db;

        /// <summary>
        /// Create object instance
        /// </summary>
        public virtual TDomainClass Create()
        {
// ReSharper disable SuggestUseVarKeywordEvident
            TDomainClass result = container.Resolve<TDomainClass>();
// ReSharper restore SuggestUseVarKeywordEvident
            return result;
        }
        /// <summary>
        /// Find entry by key
        /// </summary>
        public virtual IQueryable<TDomainClass> Find(TDomainClass instance)
        {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public virtual TDomainClass Find(long id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Get All Entites 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TDomainClass> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// Save Changes in the entities
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    var entityName = validationResult.Entry.Entity.GetType().Name;
                    errorMessages.AddRange(validationResult.ValidationErrors.Select(error => entityName + "." + error.PropertyName + ": " + error.ErrorMessage));
                }

                throw;
            }
        }
        /// <summary>
        /// Delete an entry
        /// </summary>
        public virtual void Delete(TDomainClass instance)
        {
            DbSet.Remove(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Add(TDomainClass instance)
        {
            DbSet.Add(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Update(TDomainClass instance)
        {
            DbSet.AddOrUpdate(instance);
        }

        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            db.LoadProperty(entity, propertyExpression, isCollection);
        }
        
        /// <summary>
        /// Logged in User Identity
        /// Name of Logged In User
        /// </summary>
        public string LoggedInUserIdentity
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        /// <summary>
        /// User Timezone OffSet
        /// </summary>
        public TimeSpan UserTimezoneOffSet
        {
            get
            {
                var userTimeZoneOffsetClaim = HttpContext.Current.Session["UserTimezoneOffset"]; //ClaimHelper.GetClaimsByType<string>(SmdClaimTypes.UserTimezoneOffset);
                if (userTimeZoneOffsetClaim == null)
                {
                    return TimeSpan.FromMinutes(0);
                }

                TimeSpan userTimeZoneOffset;
                TimeSpan.TryParse(userTimeZoneOffsetClaim.ToString(), out userTimeZoneOffset);

                return userTimeZoneOffset;
            }
        }

      
        #endregion

    }
}