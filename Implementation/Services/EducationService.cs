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
    class EducationService : IEducationService
    {
           #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IEducationRepository _educationRepository;

        #endregion
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public EducationService( IEducationRepository educationRepository)
        {
            this._educationRepository = educationRepository;
        }

        #endregion
        #region public

        public IEnumerable<Education> GetEducationsList()
        {
            return _educationRepository.GetAll().Where(g => g.Status != 0);
        }

        #endregion
    }
}
