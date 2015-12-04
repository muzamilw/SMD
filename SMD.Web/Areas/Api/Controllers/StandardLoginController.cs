using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using SMD.Implementation.Identity;
using SMD.MIS.Areas.Api.Models;
using SMD.Models.IdentityModels;
using SMD.Models.RequestModels;
using System;
using System.Net;
using System.Web;
using System.Web.Http;
using SMD.MIS.Areas.Api.ModelMappers;

namespace SMD.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Standard Login Api Controller 
    /// </summary>
    public class StandardLoginController : ApiController
    {
        #region Private
        
        #endregion

        #region Constructor
        
        #endregion

        #region Public

        public ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        /// <summary>
        /// Delete Profile Question 
        /// </summary>
        public async Task<WebApiUser> Post(StandardLoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            User user = await UserManager.FindAsync(request.UserName, request.Password);
            if (user == null)
            {
                throw new InvalidOperationException(LanguageResources.InvalidCredentials);
            }

            return user.CreateFrom();
        }

        #endregion
    }
}