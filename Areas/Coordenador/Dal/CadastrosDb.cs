using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chamados.Models;
namespace Chamados.DAO
{
    public class CadastrosDb
    {
        private DbChamados db = new DbChamados();
        private Utl utl = new Utl();

        #region Propriedade

        public List<ch_prioridade> ListaPrioridade()
        {
            return db.ch_prioridade.ToList();
        }

        public string[] CadastroPrioridade(ch_prioridade p)
        {
            try
            {
                db.ch_prioridade.Add(p);
                db.SaveChanges();
                utl.SetMensagem("true", "Adicionado com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }

        #endregion

        #region Tipo

        public List<ch_tipo> ListaTipo()
        {
            return db.ch_tipo.ToList();
        }

        public string[] CadastroTipo(ch_tipo t)
        {
            try
            {
                db.ch_tipo.Add(t);
                db.SaveChanges();
                utl.SetMensagem("true", "Adicionado com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }

        #endregion

        #region Status

        public List<ch_status> ListaStatus()
        {
            return db.ch_status.ToList();
        }

        public string[] CadastroStatus(ch_status s)
        {
            try
            {
                db.ch_status.Add(s);
                db.SaveChanges();
                utl.SetMensagem("true", "Adicionado com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }

        #endregion


        #region Classificação

        public List<ch_classificacao> ListaClassificacao()
        {
            return db.ch_classificacao.ToList();
        }

        public string[] CadastroClassificacao(ch_classificacao c)
        {
            try
            {
                db.ch_classificacao.Add(c);
                db.SaveChanges();
                utl.SetMensagem("true", "Adicionado com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }

        #endregion


        #region SubClassificação

        public List<ch_classificacao> DropDownClassificacao()
        {
            var lista = db.ch_classificacao.ToList();

            return lista;
        }

        public List<ch_subclassif> ListaSubClassificacao()
        {
            return db.ch_subclassif.ToList();
        }

        public string[] CadastroSubClassificacao(ch_subclassif s)
        {
            try
            {
                db.ch_subclassif.Add(s);
                db.SaveChanges();
                utl.SetMensagem("true", "Adicionado com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }


        #endregion

    }
}