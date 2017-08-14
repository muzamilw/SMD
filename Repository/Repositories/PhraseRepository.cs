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
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return DbSet.Where(i => i.SectionId == Id).ToList();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public bool CreatePhrase(Phrase phrase)
        {
            try
            {
                bool result = false;
                Phrase newPhrase = new Phrase();
                newPhrase.PhraseName = phrase.PhraseName;
                newPhrase.SectionId = phrase.SectionId;
                newPhrase.SortOrder = phrase.SortOrder;
                db.Phrase.Add(newPhrase);
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        public bool EditPhrase(Phrase phrase)
        {

            try
            {
                bool result = false;
                Phrase TobeUpdated = db.Phrase.Where(i => i.PhraseId == phrase.PhraseId).FirstOrDefault();

                TobeUpdated.PhraseName = phrase.PhraseName;
                TobeUpdated.SectionId = phrase.SectionId;
                TobeUpdated.SortOrder = phrase.SortOrder;
                db.Phrase.Attach(TobeUpdated);

                db.Entry(TobeUpdated).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
                  
        }

        public bool DeletePhrase(long phraseId)
        {

            bool result = false;
            Phrase TobeDeleted = db.Phrase.Where(i => i.PhraseId == phraseId).FirstOrDefault();
            if (TobeDeleted != null)
            {
                db.Phrase.Remove(TobeDeleted);
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        
        #endregion
    }
}
