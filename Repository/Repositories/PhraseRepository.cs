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
    public class PhraseRepository : BaseRepository<Phrase>, IPhraseRepository
    {
      


        
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PhraseRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Phrase> DbSet
        {
            get { return db.Phrase; }
        }
        #endregion
        #region Public
      
        /// <summary>
        /// Find Queston by Id 
        /// </summary>
        public Phrase Find(int id)
        {
            return DbSet.Find(id);
        }

      

        



       
        #endregion
    }
}
