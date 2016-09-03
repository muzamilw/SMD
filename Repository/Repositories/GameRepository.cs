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
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        #region Private
       
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public GameRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Game> DbSet
        {
            get { return db.Games; }
        }
        #endregion

        
        #region Public

        public Game Find(int id)
        {
            return DbSet.Where(g => g.GameId == id).SingleOrDefault();
        }

        /// <summary>
        /// Get a Random game
        /// </summary>
        public Game GetRandomGame(int ExistingGameId)
        {
            var games = DbSet.Where(g => g.GameId != ExistingGameId && g.Status == true).ToList();
            int intRandomAnswer = (new Random()).Next(1, games.Count());
            return games.ElementAt(intRandomAnswer);
        }
      
        
        #endregion
    }
}
