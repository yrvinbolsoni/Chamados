using Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chamados.Areas.Cliente.Dal;
namespace Chamados.Areas.Cliente.Controllers
{
    public class TicketController : Controller
    {
        private DbChamados db = new DbChamados();
        private ChamadosClienteDB chamadoDal = new ChamadosClienteDB();


        public ActionResult Index()
        {
            var ch_chamados = chamadoDal.BuscaChamado();
            return View(ch_chamados.ToList());
        }

        public ActionResult FinalizarChamado(int id)
        {
            AnalisaMensagem(chamadoDal.FinalizarChamado(id));
            return RedirectToAction("Detalhes/" + id);
        }

        

        public void AnalisaMensagem(string[] msg)
        {
            if (msg[0] == "true" || msg[0] =="false")
            {
                TempData["typeMensagem"] = msg[2];
                TempData["Mensagem"] = msg[1];
            }
        }

        public ActionResult BuscaRapida(FormCollection f)
        {
            var texto = f["textoBusca"];
            try
            {
                int NumeroChamado = Convert.ToInt32(texto);
                var lista = chamadoDal.BuscaRapidaPorId(NumeroChamado);
                return View("Index", lista);
            }
            catch (FormatException ex)
            {
                texto.Trim();
                if (!String.IsNullOrWhiteSpace(texto))
                {
                    var lista = chamadoDal.BuscaRapidaPorAssunto(texto);
                    return View("Index", lista);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AdicionarAnexo(List<HttpPostedFileBase> postedFile, FormCollection f)
        {
            // dados formulario 
            int Id = Convert.ToInt32(f["DeskId"]);

            // adiciona nota 
            string[] resultado = chamadoDal.AdicionarAnexos(postedFile, Id ,  null);

            AnalisaMensagem(resultado);
            return RedirectToAction("Detalhes/" + Id);
        }

        public ActionResult RemoverAnexos(int id, int IdDesk)
        {
            string[] mensagem = chamadoDal.RemoverAnexoPorId(id);
            AnalisaMensagem(mensagem);

            return RedirectToAction("Detalhes/" + IdDesk);
        }


        public ActionResult Novo()
        {
            ViewBag.tipo = new SelectList(chamadoDal.DropDownTipo(), "id", "descs");

            ViewBag.prioridade = new SelectList(chamadoDal.DropDownPrioridade(), "id", "descs" , chamadoDal.DropDownPrioridade().Where(x => x.descs =="Sem Prioridade").FirstOrDefault().id);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo([Bind(Include = "postedFile,tipo,assunto,prioridade,descricacao,com_copia")] ch_chamados chamado , List<HttpPostedFileBase> postedFile)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(chamadoDal.NovoTicket(chamado , postedFile));
                return RedirectToAction("Novo/" + chamado.id);
            }

            ViewBag.user_cli = new SelectList(chamadoDal.DropDownColab(), "id", "nome", chamado.user_cli);
            ViewBag.user_responsavel = new SelectList(chamadoDal.DropDownColab(), "id", "nome", chamado.user_responsavel);
            ViewBag.emp = new SelectList(chamadoDal.DropDownEmpresa(), "id", "descs", chamado.emp);
            ViewBag.classif = new SelectList(chamadoDal.DropDownClassificaco(), "id", "descs", chamado.classif);
            ViewBag.prioridade = new SelectList(chamadoDal.DropDownPrioridade(), "id", "descs", chamado.prioridade);
            ViewBag.statusE = new SelectList(chamadoDal.DropDownStatus(), "id", "descs", chamado.statusE);
            ViewBag.sub_classif = new SelectList(chamadoDal.DropDownSubClassificacao(), "id", "descs", chamado.sub_classif);
            ViewBag.tipo = new SelectList(chamadoDal.DropDownTipo(), "id", "descs", chamado.tipo);
            return View(chamado);
        }

        public void MostarArquivo(int id)
        {
            var dados = chamadoDal.BuscaAnexoPorId(id);  // pesquisa valor binario 

            Response.Clear();  // limpar reponses anteriores 
            Response.ContentType = dados.a_type; // extenção do arquivo 
            Response.AppendHeader("Content-Disposition", "inline; filename =" + dados.a_name + "");  // nome que vai aparecer para donwload
            Response.BufferOutput = true;
            Response.AddHeader("Content-Length", dados.a_file.Length.ToString());
            Response.BinaryWrite(dados.a_file); // convertando aquivo
            Response.End();
        }

        // view de edição
        public ActionResult Detalhes(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ch_chamados chamado = chamadoDal.VerificaExistenciaChamado(id);
            if (chamado == null)
            {
                return HttpNotFound();
            }

            var dados = chamadoDal.BuscaListaAnexoPorId(id);

            ViewBag.Anexo = dados; // se tiver dados colocar o nome das notas 

            if (dados.Count == 0)
            {
                ViewBag.Anexo = null;

            }
            ViewBag.QuantAnexo = dados.Count();

            var msg = chamadoDal.BuscaMensagensPorChamado(id);

            ViewBag.statusE = new SelectList(chamadoDal.DropDownStatus(), "id", "descs", chamado.statusE);
            ViewBag.Mensagens = msg;
            ViewBag.NumeroMensagens = msg.Where(x => x.leitura_cli == "N").Count();

            return View(chamado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // edita chamado
        public ActionResult Detalhes([Bind(Include = "id,statusE")] ch_chamados chamado)
        {
            AnalisaMensagem(chamadoDal.EditarChamado(chamado));
            return RedirectToAction("Detalhes/" + chamado.id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // adiciona mensagem no banco de dados 
        public ActionResult AdicionarMensagem(FormCollection f)
        {
            var id = Convert.ToInt64(f["IdChamado"]);
            var msg = f["msg"];
            var usuario = User.Identity.Name.ToString().Replace(@"TIYRVINB\", "");

            AnalisaMensagem(chamadoDal.AdicionaMensagem(id, msg));

            return RedirectToAction("Detalhes/" + id);
        }

        // marca mensagem como lida no banco de dados 
        public void MarcarMensagensLida(int id)
        {
            chamadoDal.MarcarMensagensLidaPorChamado(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
