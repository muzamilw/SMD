using Cash4Ads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Cash4Ads.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            User sessionVar = Session["User"] as Cash4Ads.Models.User;
            if (sessionVar != null)
            {
                ViewBag.userid = sessionVar.UserId;
                ViewBag.companyid = sessionVar.CompanyId;
            }
            else {
                ViewBag.userid = 0;
                ViewBag.companyid = 0;
            }
          
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
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://accounts.google.com/");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    string url = "o/oauth2/revoke?token=584729032378-al3tomni29c6uqh54kuph83dhdtl6lk4.apps.googleusercontent.com";
            //    var response = client.GetAsync(url);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
                    
            //    }
            //}
            Session["User"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { signedIn = 0 });
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