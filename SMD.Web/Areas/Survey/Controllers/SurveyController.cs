using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMD.MIS.Areas.Survey.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey/Survey
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
      
    }
}