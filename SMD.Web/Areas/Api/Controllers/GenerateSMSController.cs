using SMD.Interfaces.Services;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.RequestModels;
using SMD.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class GenerateSMSController : ApiController
    {
        #region Private
        private readonly IWebApiUserService webApiUserService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GenerateSMSController(IWebApiUserService webApiUserService)
        {
            if (webApiUserService == null)
            {
                throw new ArgumentNullException("webApiUserService");
            }

            this.webApiUserService = webApiUserService;
        }

        #endregion
        #region Public
        [ApiExceptionCustom]
        public GenerateSMSResponse Get(string authenticationToken, [FromUri] GenerateSmsApiRequest request)
        {
            try
            {


                if (string.IsNullOrEmpty(authenticationToken) || request == null || !ModelState.IsValid)
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
                }

                var resposne = webApiUserService.generateAndSmsCode(request.UserId, request.PhoneCountryCode+request.phoneNo);



                return new GenerateSMSResponse { Message = "Success", Status = true, VerificationCode = resposne.ToString() };

            }
            catch (Exception e)
            {

                return new GenerateSMSResponse { Message = e.ToString(), Status = false };
            }
        }

        #endregion
    }
}
