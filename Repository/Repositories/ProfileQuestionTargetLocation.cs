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
    }
}
