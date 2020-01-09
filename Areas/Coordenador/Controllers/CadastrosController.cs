using Chamados.DAO;
using Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chamados.Areas.Coordenador.Controllers
{
    public class CadastrosController : Controller
    {
        private CadastrosDb cadastroDao = new CadastrosDb();

        // GET: Cadastros
        public ActionResult Index()
        {
            return View();
        }

        #region Configuração
        public void AnalisaMensagem(string[] msg)
        {
            if (msg[0] == "true")
            {
                TempData["typeMensagem"] = msg[2];
                TempData["Mensagem"] = msg[1];
            }
        }
        #endregion
    
        #region Prioridades    

        public ActionResult Prioridades()
        {
            var lista = cadastroDao.ListaPrioridade();

            return View(lista);
        }

        public ActionResult NovaPrioridade()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaPrioridade(ch_prioridade prioridade)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(cadastroDao.CadastroPrioridade(prioridade));
            }
            else
            {
                if (String.IsNullOrEmpty(prioridade.descs))
                {
                    ModelState.AddModelError("descs", "O nome deve ser preechido");
                }
            }
            return View();
        }

        #endregion

        #region Tipo
        public ActionResult Tipo()
        {
            var lista = cadastroDao.ListaTipo();
            return View(lista);
        }

        public ActionResult NovoTipo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoTipo(ch_tipo tipo)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(cadastroDao.CadastroTipo(tipo));
            }
            return View();
        }

        #endregion

        #region Status

        public ActionResult Status()
        {
            var lista = cadastroDao.ListaStatus();
            return View(lista);
        }

        public ActionResult NovoStatus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoStatus(ch_status status)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(cadastroDao.CadastroStatus(status));
            }
            return View();
        }

        #endregion

        #region Classificacao

        public ActionResult Classificacao()
        {
            var lista = cadastroDao.ListaClassificacao();
            return View(lista);
        }

        public ActionResult NovaClassificacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaClassificacao(ch_classificacao Classificacao)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(cadastroDao.CadastroClassificacao(Classificacao));
            }
            return View();
        }
        #endregion

        #region SubClassificacao

        public ActionResult SubClassificacao()
        {
            var lista = cadastroDao.ListaSubClassificacao();
            return View(lista);
        }

        public ActionResult NovaSubClassificacao()
        {
            ViewBag.classifID = new SelectList(cadastroDao.DropDownClassificacao(), "id", "descs");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaSubClassificacao(ch_subclassif SubClassificacao)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(cadastroDao.CadastroSubClassificacao(SubClassificacao));
            }
            ViewBag.classifID = new SelectList(cadastroDao.DropDownClassificacao(), "id", "descs");

            return View();
        }

        #endregion

    }
}
