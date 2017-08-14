using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Repository
{
    public interface IPhraseRepository : IBaseRepository<Phrase, int>
    {
        List<Phrase> GetAllPhrasesByID(long Id);
        bool CreatePhrase(Phrase phrase);
        bool EditPhrase(Phrase phrase);
        bool DeletePhrase(long phraseId);
    }
}
