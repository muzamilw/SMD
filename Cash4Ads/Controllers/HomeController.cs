using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cash4Ads.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult advertisers()
        {
            return View();
        }

        public ActionResult afilliates()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult get_paid_watch_ads()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult cashout()
        {
            return View();
        }

        public ActionResult logOut()
        {
            Session["User"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}