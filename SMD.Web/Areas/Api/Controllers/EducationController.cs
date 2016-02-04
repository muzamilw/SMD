using System.Web;
using SMD.Interfaces.Services;
using System.Net;
using System.Web.Http;
using SMD.MIS.ModelMappers;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class EducationController : ApiController
    {
            #region Public
        private readonly IEducationService educationService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public EducationController(IEducationService educationService)
        {
            this.educationService = educationService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        [ApiExceptionCustom]
        public Models.EducationResponse Get(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return educationService.GetEducationsList().CreateFrom();
        }


        #endregion
    }
}
