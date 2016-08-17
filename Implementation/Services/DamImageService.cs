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
    class DamImageService : IDamImageService
    {
            #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IDamImageRepository damRepository;
    
        #endregion
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public DamImageService(IDamImageRepository _damRepository)
        {
            this.damRepository = _damRepository;
  
        }

        #endregion
        #region public
        public List<DamImage> getAllImages(int mode)
        {
            return damRepository.getAllImages(mode);
        }
        #endregion
    }
}
