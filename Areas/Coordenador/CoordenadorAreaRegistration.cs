using System.Web.Mvc;

namespace Chamados.Areas.Coordenador
{
    public class CoordenadorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Coordenador";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Coordenador_default",
                "Coordenador/{controller}/{action}/{id}",
                new { controller = "Ticket", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}