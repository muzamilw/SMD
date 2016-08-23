using System.Web.Mvc;

namespace SMD.MIS.Areas.SuperNovaDashboard
{
    public class SuperNovaDashboardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SuperNovaDashboard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SuperNovaDashboard_default",
                "SuperNovaDashboard/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}