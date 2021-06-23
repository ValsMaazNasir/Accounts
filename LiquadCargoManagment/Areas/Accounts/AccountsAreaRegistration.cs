using System.Web.Mvc;

namespace LiquadCargoManagment.Areas.Accounts
{
    public class AccountsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Accounts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            context.Routes.LowercaseUrls = true;

            context.MapRoute(
                "Accounts_default",
                "Accounts/{controller}/{action}/{id}",
                new { Controller = "Account",  action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] {
                "LiquadCargoManagment.Areas.Accounts.Controllers"
                }
            );
        }
    }
}