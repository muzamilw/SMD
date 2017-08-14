using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IGameRepository : IBaseRepository<Game, int>
    {

        Game GetRandomGame(int ExistingGameId);
        GetRandomGameByUser_Result GetRandomGameByUser(long ExistingGameId, string UserId);

        Game GetRandomGame();
        
    }


}
