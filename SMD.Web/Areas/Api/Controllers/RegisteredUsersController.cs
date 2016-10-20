using SMD.Interfaces.Services;
using SMD.Models.RequestModels;
using SMD.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SMD.MIS.Areas.Api.Controllers
{
    public class RegisteredUsersController : ApiController
    {
        #region Attributes
        private readonly ICompanyService companyService;
        #endregion

        #region Constructors
        public RegisteredUsersController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        #endregion

        #region Methods

        public RegisteredUsersResponseModel Get([FromUri] RegisteredUsersSearchRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                var result = companyService.GetRegisterdUsers(request);
                var response = new RegisteredUsersResponseModel();
                response.RegisteredUser = result.RegisteredUser;
                response.TotalCount = result.TotalCount;
                return response;
            }
        }
        #endregion
    }
}
