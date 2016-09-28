using SMD.Interfaces.Data;
using SMD.Models.Common;
using SMD.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.Franchise.Controllers
{

    //[SiteAuthorize(MisRoles = new[] { SecurityRoles.Supernova_Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSuperNovaAdmin })]
    public class DashboardController : Controller
    {
        // GET: SuperNovaDashboard/Index
        [SiteAuthorize(MisRoles = new[] { SecurityRoles.Franchise_Account_Manager, SecurityRoles.Franchise_Admin,SecurityRoles.Franchise_Approvers,SecurityRoles.Franchise_Cashout_Manager,SecurityRoles.Franchise_Creative_ }, AccessRights = new[] { SecurityAccessRight.CanViewSuperNovaAdmin })]
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// Manage Survey Question Approval Action 
        /// </summary>
        public ActionResult SurveyQuestionApproval()
        {
            return View();
        }

        /// <summary>
        /// Approval/rejection of adds
        /// </summary>
        public ActionResult AddAproval(int adType)
        {
            ViewBag.addType = adType;
            return View();
        }
        public ActionResult CouponsAproval()
        {
            return View();
        }
        public ActionResult ProfileQuestionApproval()
        {
            return View();
        }
        public ActionResult TransferRateDashboard()
        {
            return View();
        }

    }
}