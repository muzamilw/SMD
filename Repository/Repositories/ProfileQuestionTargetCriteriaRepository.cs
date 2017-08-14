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
        public bool UpdateTargetcriteria(ProfileQuestionTargetCriteria Crt)
        { 
            bool Result=false;
            ProfileQuestionTargetCriteria obj = DbSet.Where(i => i.ID == Crt.ID).FirstOrDefault();
            if (obj != null)
            {
                obj.AdCampaignAnswer = Crt.AdCampaignAnswer;
                obj.AdCampaignID = Crt.AdCampaignID;
                obj.EducationID = Crt.EducationID;
                obj.IncludeorExclude = Crt.IncludeorExclude;
                obj.IndustryID = Crt.IndustryID;
                obj.LanguageID = Crt.LanguageID;
                obj.PQAnswerID = Crt.PQAnswerID;
                obj.PQID = Crt.PQID;
                //obj.ID = Crt.ID;
                obj.Type = Crt.Type;

                db.ProfileQuestionTargetCriteria.Attach(obj);

                db.Entry(obj).State = EntityState.Modified;

                if (db.SaveChanges() > 0)
                {
                    Result = true;
                }
            }
            return Result;
        }



    }
}
