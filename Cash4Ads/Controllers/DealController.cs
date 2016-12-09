using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cash4Ads.Controllers
{
    public class DealController : Controller
    {
        // GET: Deal
        public ActionResult Index(string id)
        {
           // ViewBag.CouponId = id;
            return View("~/Views/Deal/Deals.cshtml");
        }
    }
}