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
   public  class ProfileQuestionTargetCriteriaRepository : BaseRepository<ProfileQuestionTargetCriteria>, IProfileQuestionTargetCriteriaRepository
    {
       public ProfileQuestionTargetCriteriaRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProfileQuestionTargetCriteria> DbSet
        {
            get { return db.ProfileQuestionTargetCriteria; }
        }

        public List<ProfileQuestionTargetCriteria> GetProfileQuestionTargetCiteria(long PQID)
        {

            return DbSet.Where(i => i.PQID == PQID).ToList();
        
        }

        public void RemoveCriteria(int PqID)
        {
            ProfileQuestionTargetCriteria obj = DbSet.Where(i => i.PQID == PqID).FirstOrDefault();

            if (obj != null)
            {
                DbSet.Remove(obj);
                db.SaveChanges();
            }
        
        }

    }
}
