using System.Collections.Generic;
using System.Linq;
using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class ApproveProfileQuestionAnsController : ApiController
    {

        #region Public
        private readonly IProfileQuestionAnswerService _profileQuestionAnswerService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constuctor 
        /// </summary>
        public ApproveProfileQuestionAnsController(IProfileQuestionAnswerService profileQuestionAnswerService)
        {
            _profileQuestionAnswerService = profileQuestionAnswerService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Profile Questions
        /// </summary>
        public IEnumerable<ProfileQuestionAnswer> Get([FromUri] ProfileQuestionAnswerSearchRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var domainList = _profileQuestionAnswerService.GetProfileQuestionAnswerOrderBySortorder(request.ProfileQuestionId);
            return domainList.Select(a => a.CreateFrom()).ToList();
        }
        #endregion
    }
}
