using Cash4Ads.Models;
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
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
        public ActionResult reportViewer(int reportType)
        {
            string source = "http://manage.cash4ads.com/reportViewers/";
            User objUser =  Session["User"] as User;
            if(objUser == null)
                return RedirectToAction("Index", "Home");
            if (reportType == 1)
            {
                DateTime todayDate = DateTime.Today;

                DateTime LastMonthStartDT = todayDate.AddMonths(-1);
                source += "user.aspx?userID=" + objUser.UserId + "&StartDate=" + DateTime.Now.AddMonths(-1).ToString("M-d-yyyy") + "&EndDate=" + DateTime.Now.ToString("M-d-yyyy"); //11-01-2015 //12-26-2016
            }
            else if (reportType == 2)
            {
                source += "publisher.aspx?CompanyId=" + objUser.CompanyId;
            }
            ViewBag.src = source;
            return View();
        }
    }
}