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
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
      


        
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public SectionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Section> DbSet
        {
            get { return db.Section; }
        }
        #endregion
        #region Public
      
        /// <summary>
        /// Find Queston by Id 
        /// </summary>
        public Section Find(int id)
        {
            return DbSet.Find(id);
        }

        public List<Section> GetAllSections()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Section.ToList();
        }

        public Section GetSectionbyID(int sectionID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Section.Where(i => i.SectionId == sectionID).FirstOrDefault();
        }
        #endregion
    }
}
