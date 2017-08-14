using Microsoft.Practices.Unity;
using SMD.Interfaces;
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
    public class ProfileQuestionTargetLocationRepository : BaseRepository<ProfileQuestionTargetLocation>, IProfileQuestionTargetLocationRepository
    {
        public ProfileQuestionTargetLocationRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProfileQuestionTargetLocation> DbSet
        {
            get { return db.ProfileQuestionTargetLocation; }
        }
        //public List<ProfileQuestionTargetLocation> GetPQlocation(long pqId)
        //{
        //    db.Configuration.LazyLoadingEnabled = false;
        //    return DbSet.Include(co => co.Country).Include(d => d.City).Where(pq => pq.PQID == pqId).ToList();
        //}
        public bool UpdateTargetLocation(ProfileQuestionTargetLocation Tlocation)
        {
            bool Result = false;
            ProfileQuestionTargetLocation obj = DbSet.Where(i => i.ID == Tlocation.ID).FirstOrDefault();
            if (obj != null)
            {
                obj.CityID = Tlocation.CityID;
                obj.CountryID = Tlocation.CountryID;
                obj.IncludeorExclude = Tlocation.IncludeorExclude;
                obj.PQID = Tlocation.PQID;
                obj.Radius = Tlocation.Radius;
              
                db.ProfileQuestionTargetLocation.Attach(obj);

                db.Entry(obj).State = EntityState.Modified;

                if (db.SaveChanges() > 0)
                {
                    Result = true;
                }
            }
            return Result;
        }

        public bool DeleteTargetLocation(int PQID)
        {
            bool flag = false;
            ProfileQuestionTargetLocation obj = DbSet.Where(i => i.PQID == PQID).FirstOrDefault();
            if (obj != null)
            {
                db.ProfileQuestionTargetLocation.Remove(obj);
            
            }
            if (db.SaveChanges() > 0)
            {
                flag = true;
            
            }
            return flag;
        }
    }
}
