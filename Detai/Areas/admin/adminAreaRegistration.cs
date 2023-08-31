using System.Web.Mvc;

namespace Detai.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "adminlogin",
               "admin/login",
               new { controller = "Auth", action = "Login", id = UrlParameter.Optional }
           );

            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new {controller="Default", action = "Index", id = UrlParameter.Optional }
            );  
        }
    }
}