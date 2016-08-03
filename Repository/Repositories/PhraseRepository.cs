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
        public List<Phrase> GetAllPhrasesByID(long Id)
        {
            return db.Phrase.Where(i => i.PhraseId == Id).ToList();
        }
        public bool CreatePhrase(Phrase phrase)
        {
            bool result=false;
            Phrase newPhrase = new Phrase();
            newPhrase.PhraseName=phrase.PhraseName;
            newPhrase.SectionId=phrase.SectionId;
            newPhrase.SortOrder = phrase.SortOrder;
            db.Phrase.Add(newPhrase);
            if (db.SaveChanges() > 0)
            {
                result = true;
            }
            return result;
        }

        public bool EditPhrase(Phrase phrase)
        {
            bool result = false;
            Phrase newPhrase = new Phrase();
            newPhrase.PhraseName = phrase.PhraseName;
            newPhrase.SectionId = phrase.SectionId;
            newPhrase.SortOrder = phrase.SortOrder;
            db.Phrase.Attach(newPhrase);
            db.Entry(newPhrase).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                result = true;
            }

            return result;
        }

        
        #endregion
    }
}
