using Chamados.DAO;
using Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Chamados.Areas.Coordenador.Controllers
{
    public class TicketController : Controller
    {
        private DbChamados db = new DbChamados();
        
        private ChamadosDb chamadoDal = new ChamadosDb();
        // GET: Coordenador/Chamados

        public ActionResult Index()
        {
            var chamados = chamadoDal.BuscaChamado();
            // 4 Em analise com

            PainelAnaliseCordenador();

            return View(chamados.ToList());
        }


        public ActionResult BuscaChamados()
        {

            List<ch_chamados> ch = new List<ch_chamados>();

            ViewBag.emp = new SelectList(chamadoDal.DropDownEmpresaTodas(), "id", "descs");
            ViewBag.statusE = new SelectList(chamadoDal.DropDownStatusTodos(), "id", "descs");
            ViewBag.classif = new SelectList(chamadoDal.DropDownClassificacoTodos(), "id", "descs");
            ViewBag.user_cli = new SelectList(chamadoDal.DropDownColaboradorTodos(), "id", "descs");
            ViewBag.user_responsavel = new SelectList(chamadoDal.SuporteResponsavelTodos(), "id", "descs");

            return View(ch);
        }

        [HttpPost]
        public ActionResult BuscaChamados( ch_chamados c)
        {
            DateTime Ate = Convert.ToDateTime(c.dt_encerramento);

            List<ch_chamados> ch = new List<ch_chamados>();

            ViewBag.emp = new SelectList(chamadoDal.DropDownEmpresaTodas(), "id", "descs");
            ViewBag.statusE = new SelectList(chamadoDal.DropDownStatusTodos(), "id", "descs");
            ViewBag.classif = new SelectList(chamadoDal.DropDownClassificacoTodos(), "id", "descs");
            ViewBag.user_cli = new SelectList(chamadoDal.DropDownColaboradorTodos(), "id", "descs");
            ViewBag.user_responsavel = new SelectList(chamadoDal.SuporteResponsavelTodos(), "id", "descs");

            var busca = chamadoDal.BuscaDetalhadaChamados(c , Ate);

            return View(busca);
        }
        




        private void PainelAnaliseCordenador()
        {
            var DiaAtualMenos2 = DateTime.Now.AddDays(-2);
            var chamadoAnalise = (from p in db.ch_chamados
                                  where p.statusE == 4
                                  where p.user_responsavel != null
                                  where p.CAD_COLABORADOR1.tipo_u == 1002 || p.CAD_COLABORADOR1.tipo_u == 1003
                                  group p by new { p.CAD_COLABORADOR1.nome, p.user_responsavel } into g
                                  select new UserAnalise
                                  {
                                      IdReponsavel = g.Key.user_responsavel,
                                      NomeUser = g.Key.nome,
                                      Quantidade = g.Count(),
                                      DiasParado = g.Count(x => x.dt_abertura <= DiaAtualMenos2)
                                  }).ToList();

            ViewBag.ChamadoEmAnalise = chamadoAnalise;
        }

        public ActionResult BuscaRapida(FormCollection f)
        {
            var texto =  f["textoBusca"];
            try
            {
                int NumeroChamado = Convert.ToInt32(texto);
                var lista = db.ch_chamados.Where(x => x.id == NumeroChamado).ToList();
                PainelAnaliseCordenador();
                return View("Index", lista);

            }
            catch (FormatException ex)
            {
                texto.Trim();
                if (!String.IsNullOrWhiteSpace(texto))
                {
                var lista = db.ch_chamados.Where(x => x.assunto.Contains(texto)).ToList();
                PainelAnaliseCordenador();
                return View("Index", lista);
                }
                return RedirectToAction("Index");
            }
        }

        public PartialViewResult ChamadosEmAnalisePorUsuario(int id)
        {
            // 4 igual chamados em analise 
            var lista = db.ch_chamados.Where(x => x.user_responsavel == id && x.statusE == 4 ).ToList();
            return PartialView("_ChamadosAnalise", lista);
        }
        
        public class UserAnalise
        {
            public string NomeUser { get; set; }
            public int Quantidade { get; set; }
            public int DiasParado { get; set; }
            public int? IdReponsavel { get; set; }
        }


        public List<ch_chamados> ChamadosAnalisePorColab()
        {
            var lista = db.ch_chamados.Where(x => x.user_responsavel == 52).ToList();
            return lista;
           
        }
        // lista os chamados para atribuir
        public ActionResult TicketsAberto()
        {
            ViewBag.user_responsavel = new SelectList(chamadoDal.SuporteResponsavel(), "id", "nome");
            ViewBag.classif = new SelectList(chamadoDal.DropDownClassificaco(), "id", "descs");
            ViewBag.sub_classif = new SelectList(chamadoDal.DropDownSubClassificacao(), "id", "descs");


            var ch_chamados = chamadoDal.BuscaChamado();
            ch_chamados =   db.ch_chamados
              .Where(x => x.user_responsavel == null)
              .ToList();

            return View(ch_chamados);
        }

        public JsonResult GetSubClassificacao(int id)
        {
            return Json(chamadoDal.GetDropDownSubClassificacao(id).Select(x => new
            {
                id = x.classifID,
                desc = x.descs
            }).ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetUserPorEmpresa(int id)
        {
            return Json(chamadoDal.UsuarioPorEmpresa(id).Select(x => new
            {
                id = x.id,
                desc = x.nome
            }).ToList(), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult ClassificarTicket(FormCollection f)
        {
            int usuarioResponsavel = Convert.ToInt32(f["user_responsavel"]);
            int classificacao = Convert.ToInt32(f["classif"]);
            int subClassificacao = Convert.ToInt32(f["sub_classif"]);
            long id = Convert.ToInt64(f["item.id"]);

            chamadoDal.AtribuirTicket(usuarioResponsavel , classificacao ,  subClassificacao , id);
            return RedirectToAction("TicketsAberto");
        }
            

        public void AnalisaMensagem(string[] msg)
        {
            if (msg[0] == "true" || msg[0] == "false")
            {
                TempData["typeMensagem"] = msg[2];
                TempData["Mensagem"] = msg[1];
            }
        }

        [HttpPost]
        public ActionResult AdicionarAnexo(List<HttpPostedFileBase> postedFile, FormCollection f)
        {
            // dados formulario 
            int Id = Convert.ToInt32(f["DeskId"]);

            // adiciona nota 
            string[] resultado = chamadoDal.AdicionarAnexos(postedFile, Id , null);

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
            DropDownNovoTicket();
            return View();
        }

        private void DropDownNovoTicket()
        {
            ViewBag.user_cli = new SelectList(chamadoDal.DropDownColaMatrix(), "id", "nome");
            ViewBag.user_responsavel = new SelectList(chamadoDal.SuporteResponsavel(), "id", "nome");
            ViewBag.emp = new SelectList(chamadoDal.DropDownEmpresa(), "id", "descs");
            ViewBag.classif = new SelectList(chamadoDal.DropDownClassificaco(), "id", "descs");
            ViewBag.prioridade = new SelectList(chamadoDal.DropDownPrioridade(), "id", "descs");
            ViewBag.statusE = new SelectList(chamadoDal.DropDownStatus(), "id", "descs");
            ViewBag.sub_classif = new SelectList(chamadoDal.DropDownSubClassificacao(), "id", "descs");
            ViewBag.tipo = new SelectList(chamadoDal.DropDownTipo(), "id", "descs");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo([Bind(Include = "postedFile,id,emp,user_cli,prioridade,statusE,tipo,classif,sub_classif,assunto,descricacao,com_copia,user_responsavel")] ch_chamados chamado, List<HttpPostedFileBase> postedFile)
        {
            if (ModelState.IsValid)
            {
                AnalisaMensagem(chamadoDal.NovoTicket(chamado, postedFile));
                return RedirectToAction("Novo");
            }
            DropDownNovoTicket();
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
            var colab = db.CAD_COLABORADOR.Where(x => x.id == chamado.user_cli).FirstOrDefault();
            ViewBag.statusE = new SelectList(chamadoDal.DropDownStatus(), "id", "descs", chamado.statusE);
            ViewBag.Mensagens = msg;
            ViewBag.NumeroMensagens = msg.Where(x => x.leitura_suporte == "N").Count();

            ViewBag.user_responsavel = new SelectList(chamadoDal.DropDownColab(), "id", "nome", chamado.user_responsavel);
            ViewBag.classif = new SelectList(chamadoDal.DropDownClassificaco(), "id", "descs", chamado.classif);
            ViewBag.sub_classif = new SelectList(chamadoDal.DropDownSubClassificacao(), "id", "descs", chamado.sub_classif);

            ViewBag.Ramal = (colab.ramal != null  ? colab.IN_VOIP.ramal : colab.ramal);
            ViewBag.Ip = (colab.IN_DESKTOP.ip != null ? colab.IN_DESKTOP.ip : "");
            ViewBag.Celular = (colab.SMARTPHONE != null ? colab.IN_SMARTPHONE.IN_LINHA_MOVEL.DESCS : "");


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
