using System.Web.Mvc;

namespace SMD.MIS.Areas.Ads.Controllers
{
    public class AdsController : Controller
    {
        // GET: Ads/Ads
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Approval/rejection of adds
        /// </summary>
        public ActionResult AddAproval()
        {
            return View();
        }
    }
}