using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.ResponseModels;

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

        public EducationResponse GetEducationsList()
        {
            var educations = _educationRepository.GetAllAvailable();
            return new EducationResponse
                   {
                       Educations = educations,
                       Status = true,
                       Message = LanguageResources.Success
                   };
        }

        #endregion
    }
}
