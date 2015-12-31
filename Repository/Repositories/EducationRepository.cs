using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Repository.Repositories
{

    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        #region Private

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public EducationRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Education> DbSet
        {
            get { return db.Educations; }
        }
        #endregion
        #region Public

        /// <summary>
        /// Find Language by Id 
        /// </summary>
        public Education Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get List of Language 
        /// </summary>
        public IEnumerable<Education> GetAllEducations()
        {
            return DbSet.Select(indu => indu).ToList();
        }

        /// <summary>
        /// Get List of searched Language 
        /// </summary>
        public IEnumerable<Education> SearchEducation(string searchString)
        {
            return DbSet.Where(lang => lang.Title.Contains(searchString)).ToList();
        }
        #endregion
    }
}
