using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.Franchise.Controllers
{
    public class ApprovePayPalController : Controller
    {
        // GET: Franchise/ApprovePayPal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PayPalDashboard()
        {
            return View();
        }

    }
}