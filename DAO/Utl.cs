using Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chamados.DAO
{
    public class Utl
    {
        private  CAD_COLABORADOR UserLogado = new CAD_COLABORADOR();
        public DbChamados db = new DbChamados();

        public string NomeUserLogado()
        {
            // producação VMWEB
            // desenvolvimento  TIYRVINB

            string nome = HttpContext.Current.User.Identity.Name.Replace(@"TIYRVINB\", "").ToString();
            return nome;
        }

        public CAD_COLABORADOR GetUser()
        {
            string nome =  NomeUserLogado().ToString();
            UserLogado = db.CAD_COLABORADOR.Where(x => x.username == nome).FirstOrDefault();
            return UserLogado;
        }

        private string[] mensagem = new string[3];

        // mensagens 
        public void SetMensagem(string status, string msg, string style)
        {
            // define mensagens para view

            mensagem[0] = status;
            mensagem[1] = msg;
            mensagem[2] = String.Format("alert alert-{0}  alert-dismissible", style);
        }

        public string[] GetMensagem()
        {
            return mensagem;
        }
    }
}