using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Chamados.DAO;
using Chamados.Models;
namespace Chamados.Controllers
{
    public class HomeController : Controller
    {
        private Utl utl = new Utl();

        public ActionResult Index()
        {
            CAD_COLABORADOR colab = utl.GetUser();
            switch (colab.CAD_TIPO_USER.descs)
            {
                case "Coordenador":
                return RedirectToAction("index", "Ticket", new { area = "Coordenador" });

                case "Suporte":
                    return RedirectToAction("index", "Ticket", new { area = "Coordenador" });

                case "Colaborador":
                    return RedirectToAction("index", "Ticket", new { area = "Cliente" });

                case null:
                  return RedirectToAction("index", "Ticket", new { area = "Cliente" });

                default:
                return RedirectToAction("index", "Ticket", new { area = "Cliente" });
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}