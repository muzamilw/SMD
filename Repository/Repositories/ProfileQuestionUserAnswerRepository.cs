﻿using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using SMD.Interfaces.Repository;
using SMD.Models.DomainModels;
using SMD.Repository.BaseRepository;

namespace SMD.Repository.Repositories
{
    /// <summary>
    /// ProfileQuestionUserAnswers Repository 
    /// </summary>
    public class ProfileQuestionUserAnswerRepository : BaseRepository<ProfileQuestionUserAnswer>,
        IProfileQuestionUserAnswerRepository
    {
        #region Private

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProfileQuestionUserAnswer> DbSet
        {
            get { return db.ProfileQuestionUserAnswers; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        public ProfileQuestionUserAnswerRepository(IUnityContainer container)
            : base(container)
        {

        }

        #endregion

        #region Public


        #endregion

    }
}
