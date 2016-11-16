using SMD.Models.DomainModels;
using System.Collections.Generic;

namespace SMD.Interfaces.Repository
{
    public interface IGameRepository : IBaseRepository<Game, int>
    {

        Game GetRandomGame(int ExistingGameId);


        Game GetRandomGame();
        
    }


}
