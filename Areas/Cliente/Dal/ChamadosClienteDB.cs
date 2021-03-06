﻿using Chamados.DAO;
using Chamados.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Chamados.Areas.Cliente.Dal
{   
    public class ChamadosClienteDB
    {
        private DbChamados db = new DbChamados();
        private Utl utl = new Utl();

        private readonly CAD_COLABORADOR  UserLogado = new CAD_COLABORADOR();

        public   ChamadosClienteDB()
        {
            string nome = utl.NomeUserLogado().ToString();
            this. UserLogado = db.CAD_COLABORADOR.Where(x => x.username == nome).FirstOrDefault();
        }

        // busca para trazer os chamados que estao com o usuario na parte de MEUS CHAMADOS
        public List<ch_chamados> BuscaChamado()
        {
            var lista = db.ch_chamados
                .Where(x => x.user_cli == UserLogado.id)
                .Where(x => x.statusE != 7 )
                .ToList();
            return lista;
        }
        // busca rapida para trazer os chamados pelo numero do chamado tambem pelo o usuario que esta logado 
        public List<ch_chamados> BuscaRapidaPorId(int id)
        {
           var lista =  db.ch_chamados.Where(x => x.id == id &&  x.user_cli == UserLogado.id).ToList();
           return lista;
        }

        // busca rapida para trazer os chamados pelo ASSUNTO  restringuindo tambem pelo o usuario que esta logado 
        public List<ch_chamados> BuscaRapidaPorAssunto(string assunto)
        {
            var lista = db.ch_chamados.Where(x => x.assunto.Contains(assunto) && x.user_cli == UserLogado.id).ToList();
            return lista;

        }


        public string[] NovoTicket(ch_chamados c , List<HttpPostedFileBase> postedFile)
        {
            try
            {
                c.emp = (int)UserLogado.emp;
                c.user_cli = UserLogado.id;
                c.statusE = 4;

                c.dt_abertura = DateTime.Now;
                db.ch_chamados.Add(c);
                db.SaveChanges();

                // verifica se possue anexos
                if (postedFile.FirstOrDefault() != null)
                {
                    // recupera o chamado para colocar o id
                    var RecuperarChamado = db.ch_chamados.Where(x => x.dt_abertura == c.dt_abertura || x.assunto == c.assunto).FirstOrDefault();
                    // verificar se o chamado reamente existe
                    if (RecuperarChamado != null)
                    {
                        // adiciona de fato na tabela 
                        AdicionarAnexos(postedFile, RecuperarChamado.id, "* Chamado Aberto com sucesso !");
                        return utl.GetMensagem();
                    }
                }

                utl.SetMensagem("true", "Chamado Aberto com sucesso ", "success");
                return utl.GetMensagem();

            }
            catch (Exception ex)
            {

                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }

        public string[] FinalizarChamado(int id)
        {
            ch_chamados chamado = db.ch_chamados.Where(x => x.id == id).FirstOrDefault();

            if (chamado == null )
            {
                utl.SetMensagem("false", "Chamado Invalido" , "danger");
                return utl.GetMensagem();
            }

            if (chamado.statusE == 7 )
            {
                utl.SetMensagem("false", "Este chamado já esta finalizado", "danger");
                return utl.GetMensagem();


            }
            try
            {
                chamado.statusE = 7;
                chamado.dt_encerramento = DateTime.Now;
                db.Entry(chamado).State = EntityState.Modified;
                db.SaveChanges();
                utl.SetMensagem("true", "Chamado Finalizado", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex )
            {
                utl.SetMensagem("false", ex.Message , "danger");
                return utl.GetMensagem();
            }
        }



        #region DropDown


        public List<CAD_COLABORADOR> DropDownColab()
        {
            var lista = db.CAD_COLABORADOR.ToList();
            return lista;
        }

        public List<CAD_COLABORADOR> DropDownResponsavel()
        {
            var lista = db.CAD_COLABORADOR.ToList();
            return lista;
        }


        public List<CAD_EMP> DropDownEmpresa()
        {
            var lista = db.CAD_EMP.ToList();
            return lista;
        }

        public List<ch_classificacao> DropDownClassificaco()
        {
            var lista = db.ch_classificacao.ToList();
            return lista;
        }

        public List<ch_prioridade> DropDownPrioridade()
        {
            var lista = db.ch_prioridade.ToList();
            return lista;
        }

        public List<ch_subclassif> DropDownSubClassificacao()
        {
            var lista = db.ch_subclassif.ToList();
            return lista;
        }

        public List<ch_tipo> DropDownTipo()
        {
            var lista = db.ch_tipo.ToList();
            return lista;
        }


        public List<ch_status> DropDownStatus()
        {
            var lista = db.ch_status.ToList();
            return lista;
        }


        #endregion

        #region TrocaDeMensagens

        public string[] AdicionaMensagem(long id, string msg)
        {
            try
            {
                ch_msg mensaguem = new ch_msg()
                {
                    msg = msg,
                    dt_post = DateTime.Now,
                    id_chamado = id,
                    id_user = UserLogado.id,
                    leitura_cli = "N",
                    leitura_suporte = "N",


                };

                db.ch_msg.Add(mensaguem);
                db.SaveChanges();
                utl.SetMensagem("true", "Mensagem adicionada com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }



        public List<ch_msg> BuscaMensagensPorChamado(long? id)
        {
            var lista = db.ch_msg.Where(x => x.id_chamado == id).ToList();

            return lista;
        }

        public void MarcarMensagensLidaPorChamado(int id)
        {
            var msg = db.ch_msg.Where(x => x.id_chamado == id);

            foreach (var m in msg)
            {
                m.leitura_cli = "S";
                db.Entry(m).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        #endregion

        #region Anexos

        public ch_anexo BuscaAnexoPorId(int id)
        {
            var anexo = db.ch_anexo.Where(x => x.id == id).FirstOrDefault();
            return anexo;
        }

        public List<ch_anexo> BuscaListaAnexoPorId(long? id)
        {
            var anexo = db.ch_anexo.Where(x => x.id_chamado == id).ToList();
            return anexo;
        }



        public string[] AdicionarAnexos(List<HttpPostedFileBase> postedFile, long Id , string msg)
        {

            // convertar arquivo em byte 
            // adiciona um ou mais anexos 
            string Notificao = msg;
            foreach (var anexos in postedFile)
            {
                string NameFile = anexos.FileName;
                string ContextType = anexos.ContentType;
                string FileExtension = Path.GetExtension(anexos.FileName);
                int FileSize = anexos.ContentLength;

                if (FileSize <= 2000000)
                {
                    Stream stream = anexos.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] Arquivo = binaryReader.ReadBytes((int)stream.Length); // arquivo convertindo em byte 

                    ch_anexo anexo = new ch_anexo();
                    anexo.a_name = NameFile;
                    anexo.a_type = ContextType;
                    anexo.a_file = Arquivo;
                    anexo.id_chamado = Id;
                    anexo.dt_insert = DateTime.Now;

                    db.ch_anexo.Add(anexo);
                    db.SaveChanges();

                    Notificao += String.Format("* {0} adicionado com Sucesso",NameFile);

                }
                else
                {
                    Notificao += String.Format("* danger {0} Houve um erro ao adiconar TAMANHO MAX 2MB", NameFile);  
                }
            }

            utl.SetMensagem("true", Notificao ,"success");
            return utl.GetMensagem();
        }

        public string[] RemoverAnexoPorId(int id)
        {
            // verificar se existe anexo para este desktop
            var dados = db.ch_anexo.Where(x => x.id.Equals(id)).FirstOrDefault();

            if (dados != null)
            {
                db.ch_anexo.Remove(dados);
                db.SaveChanges();
                utl.SetMensagem("true", "Anexo removido com sucesso", "success");
                return utl.GetMensagem();
            }
            else
            {
                utl.SetMensagem("true", "Nao existe Anexo atrelado a este chamado", "warning");
                return utl.GetMensagem();
            }

        }


        public string[] EditarChamado(ch_chamados c)
        {
            try
            {
                ch_chamados Chamado = db.ch_chamados.Where(x => x.id == c.id).FirstOrDefault();
                Chamado.statusE = c.statusE;

                db.Entry(Chamado).State = EntityState.Modified;
                db.SaveChanges();

                utl.SetMensagem("true", "Status Alterado com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex)
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }

        }
        #endregion

        public ch_chamados VerificaExistenciaChamado(long? id)
        {
            var chamado = db.ch_chamados.Find(id);
            return chamado;
        }
    }
}