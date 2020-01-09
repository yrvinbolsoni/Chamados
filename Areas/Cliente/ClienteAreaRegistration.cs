using System.Web.Mvc;

namespace Chamados.Areas.Cliente
{
    public class ClienteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cliente";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cliente_default",
                "Cliente/{controller}/{action}/{id}",
                new { controller = "Ticket" , action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}