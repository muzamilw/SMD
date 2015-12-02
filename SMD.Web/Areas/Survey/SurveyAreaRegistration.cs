using System.Web.Mvc;

namespace SMD.MIS.Areas.Survey
{
    public class SurveyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Survey";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Survey_default",
                "Survey/{action}/{id}",
                new {controller ="Survey" ,action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}