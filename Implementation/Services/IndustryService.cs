using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.ResponseModels;

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

        public IndustryResponse GetIndustryList()
        {
            var industries = _indRepository.GetAllAvailable();
            return new IndustryResponse
                   {
                       Industries = industries,
                       Status = true,
                       Message = LanguageResources.Success
                   };
        }

        #endregion
    }
}
