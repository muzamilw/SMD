using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;
using System;
using System.Data.Entity;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// Country Repository 
    /// </summary>
    public class DamImageRepository : BaseRepository<DamImage>, IDamImageRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public DamImageRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DamImage> DbSet
        {
            get { return db.DamImage; }
        }
        #endregion

        
        #region Public


        public DamImage Find(long id)
        {
            return db.DamImage.Where(g => g.ImageId == id).SingleOrDefault();
        }


        #endregion
    }
}
