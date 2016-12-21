using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
     public class GameService : IGameService
    {

         private readonly IGameRepository _gameRepository;
         public GameService(IGameRepository _gameRepository)
        {
            this._gameRepository = _gameRepository;
        }

         public Game GetRandomGame(int ExistingGameId)
        {
            return _gameRepository.GetRandomGame(ExistingGameId);
        }



         public GetRandomGameByUser_Result GetRandomGameByUser(long ExistingGameId,string UserId)
         {
             return _gameRepository.GetRandomGameByUser(ExistingGameId, UserId);
         }
    
    }
}
