using System.Net;
using System.Threading.Tasks;
using System.Web;
using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using System;
using System.Web.Http;
using SMD.MIS.Areas.Api.ModelMappers;
using SMD.Models.ResponseModels;
using SMD.WebBase.Mvc;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// User Balance Inquiry Api Controller 
    /// </summary>
    public class UserBalanceInquiryController : ApiController
    {
        #region Private

        private readonly IWebApiUserService webApiUserService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UserBalanceInquiryController(IWebApiUserService webApiUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Login
        /// </summary>
        [ApiExceptionCustom]
        public async Task<UserBalanceInquiryResponse> Get(string authenticationToken, [FromUri] UserBalanceInquiryRequest request)
        {
            if (request == null || !ModelState.IsValid || string.IsNullOrEmpty(authenticationToken))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            LoginResponse response = await webApiUserService.GetById(request.UserId);
            return response.CreateFromForBalance();
        }

        #endregion
    }
}