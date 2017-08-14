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
using SMD.WebBase.Mvc;
using SMD.Implementation.Identity;
using SMD.Interfaces.Data;

using SMD.Models.Common;
using SMD.Common;


namespace SMD.MIS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
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

        /// <summary>
        /// Welcome Page
        /// </summary>

       
        //[SiteAuthorize(MisRoles = new[] { SecurityRoles.EndUser_Admin, SecurityRoles.EndUser_Accounts, SecurityRoles.EndUser_Creative, SecurityRoles.Franchise_Account_Manager, SecurityRoles.Franchise_Admin, SecurityRoles.Franchise_Approvers, SecurityRoles.Franchise_Cashout_Manager, SecurityRoles.Franchise_Creative_ }, AccessRights = new SecurityAccessRight{  })]
        public ActionResult Welcome()

        {
            IEnumerable<SmdRoleClaimValue> roleClaim = ClaimHelper.GetClaimsByType<SmdRoleClaimValue>(SmdClaimTypes.Role);
            string RoleName = roleClaim != null && roleClaim.Any() ? roleClaim.ElementAt(0).Role : "Role Not Loaded";

            if (roleClaim == null && roleClaim.Any() == false)
            {
                return RedirectToAction("Login", "Account");
            }

           

             
            if (RoleName.StartsWith("Franchise"))
                return RedirectToLocal("/Franchise/Dashboard/Index");

            if (RoleName.StartsWith("Supernova"))
               return RedirectToLocal("/Supernova/Dashboard/Index");

            ViewBag.isUser = true;
           
            return View();
        }
        

        private ActionResult RedirectToLocal(string returnUrl)
        {

            //if (UserManager.LoggedInUserRole !=  "" && UserManager.LoggedInUserRole != (string)Roles.User)
            //{
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Welcome", "Home", new { area = "" });
            }
            //}
            //else
            //{
            //    return RedirectToAction("Index","Ads", new { area = "Ads" });
            //}
        }
        public ActionResult StartUpGuide()
        {
            return View();
        }
        public ActionResult TrainingVideos()
        {
            return View();
        }
        public ActionResult HelpCenter()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }

    }
}
