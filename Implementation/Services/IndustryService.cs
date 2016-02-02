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
    class IndustryService : IIndustryService
    {    
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IIndustryRepository _indRepository;

        #endregion
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public IndustryService(IIndustryRepository indRepository)
        {
            this._indRepository = indRepository;
        }

        #endregion
        #region public

        public IEnumerable<Industry> GetIndustryList()
        {
            return _indRepository.GetAll().Where(g => g.Status != 0);
        }

        #endregion
    }
}
