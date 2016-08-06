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
    public class SectionService : ISectionService
    {
      private readonly ISectionRepository _sectionRepository;
      public SectionService(ISectionRepository _sectionRepository)
      {
          this._sectionRepository = _sectionRepository;
      }
      public List<Section> GetAllSections()
      {
          return _sectionRepository.GetAllSections();
      }

    }
}
