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
     public class IndustryRepository : BaseRepository<Industry>, IIndustryRepository
    {
        #region Private
      
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
         public IndustryRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
         protected override IDbSet<Industry> DbSet
        {
            get { return db.Industries; }
        }
        #endregion
        #region Public
      
        /// <summary>
        /// Find Language by Id 
        /// </summary>
         public Industry Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get List of Language 
        /// </summary>
        public IEnumerable<Industry> GetAllLanguages()
        {
            return DbSet.Select(indu => indu).ToList();
        }

        /// <summary>
        /// Get List of searched Language 
        /// </summary>
        public IEnumerable<Industry> SearchIndustries(string searchString)
        {
           return DbSet.Where(lang => lang.IndustryName.Contains(searchString)).ToList();
        }
        #endregion
    }
}
