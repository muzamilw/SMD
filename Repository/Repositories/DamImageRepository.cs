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

        public List<DamImage> getAllImages(int mode, out int companyId)
        {
            companyId = 0;
            var user = db.Users.Where(g => g.Id == LoggedInUserIdentity).SingleOrDefault();
            if (user == null)
                return null;
            companyId = user.CompanyId.Value;
            return db.DamImage.Where(g => g.CompanyId == user.CompanyId && g.ImageCategory == mode).ToList();
        }

        #endregion
    }
}
