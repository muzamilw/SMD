using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Profile Question Base Data Api Controller 
    /// </summary>
    public class ProfileQuestionBaseController : ApiController
    {
        #region Public
        private readonly IProfileQuestionService _profileQuestionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public ProfileQuestionBaseController(IProfileQuestionService profileQuestionService)
        {
            _profileQuestionService = profileQuestionService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public ProfileQuestionBaseResponse Get()
        {
            return _profileQuestionService.GetProfileQuestionBaseData().CreateFrom();
        }

        #endregion
    }
}