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
     public class PhraseService : IPhraseService
    {

         private readonly IPhraseRepository _phraseRepository;
        public PhraseService(IPhraseRepository _phraseRepository)
        { 
          this._phraseRepository= _phraseRepository;
        }
        
        public List<Phrase> GetAllPhrasesByID(long Id)
        {
            return _phraseRepository.GetAllPhrasesByID(Id);
        }
        public bool CreatePhrase(Phrase phrase)
        {
            return _phraseRepository.CreatePhrase(phrase);
        }
        public bool EditPhrase(Phrase phrase)
        {
            return _phraseRepository.EditPhrase(phrase);
        }
    }
}
