using System.Web;
using SMD.Interfaces.Services;
using System.Net;
using System.Web.Http;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class IndustryController : ApiController
    {
          #region Public
        private readonly IIndustryService industryService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public IndustryController(IIndustryService industryService)
        {
            this.industryService = industryService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        [ApiExceptionCustom]
        public IndustryResponse Get(string authenticationToken)
        {
            if (string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return industryService.GetIndustryList().CreateFrom();
        }


        #endregion
    }
}
