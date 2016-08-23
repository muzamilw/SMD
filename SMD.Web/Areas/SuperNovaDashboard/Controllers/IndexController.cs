using SMD.Interfaces.Data;
using SMD.Models.Common;
using SMD.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.SuperNovaDashboard.Controllers
{

    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Supernova_Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSuperNovaAdmin })]
    public class IndexController : Controller
    {
        // GET: SuperNovaDashboard/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}