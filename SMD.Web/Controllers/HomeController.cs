using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SMD.Models.Common;
using SMD.Implementation;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;
using SMD.Interfaces;

namespace SMD.MIS.Controllers
{
    public class HomeController : Controller
    {

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [Dependency]
        public IClaimsSecurityService ClaimsSecurityService { get; set; }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Redirect()
        {
            return RedirectToRoute("Login");
        }

        public async Task<ActionResult> LoginFull(string token)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<ActionResult> Logout()
        {
            Thread.CurrentPrincipal = null;
            HttpContext.User = null;
            AuthenticationManager.SignOut(new[] { DefaultAuthenticationTypes.ApplicationCookie });
            return Redirect(ConfigurationManager.AppSettings["MPCDashboardPath"] + "/logout");

        }

        /// <summary>
        /// Page Under Construction
        /// </summary>
        public ActionResult PageUnAvailable()
        {
            return View();
        }

    }
}
