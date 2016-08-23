using System.Web.Mvc;

namespace SMD.MIS.Areas.SuperNovaDashboard
{
    public class SuperNovaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SuperNova";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SuperNova_default",
                "SuperNova/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}