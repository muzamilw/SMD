using System.Web.Mvc;

namespace SMD.MIS.Areas.ProfileQuestion.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /ProfileQuestion/Manage/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ProfileQuestion/Manage/
        public ActionResult StripeExample()
        {
            return View();
        }
	}
}