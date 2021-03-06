﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Chamados.Models;
using Chamados.Models.Dropdown;


namespace Chamados.DAO
{
    public class ChamadosDb
    {
        private DbChamados db = new DbChamados();
        private Utl utl = new Utl();

        private readonly CAD_COLABORADOR UserLogado = new CAD_COLABORADOR();

        public ChamadosDb()
        {
            string nome = utl.NomeUserLogado().ToString();
            this.UserLogado = db.CAD_COLABORADOR.Where(x => x.username == nome).FirstOrDefault();
         }

    public List<ch_chamados> BuscaChamado()
        {
            var lista = db.ch_chamados
                .Where(x => x.user_responsavel == UserLogado.id)
                .Where(x => x.statusE != 7) 
                .ToList();
            return lista;

        }


        public List<ch_chamados> BuscaDetalhadaChamados(ch_chamados p , DateTime ate)
        {
            DateTime dt = new DateTime();

            if (p.dt_abertura != dt && ate != dt)
            {

            }
            //DateTime teste2 = dataInicial.ToString("dd/MM/yyyy");
            if (ate == new DateTime())
                ate = DateTime.Now;

            var Buscando = (from ch in db.ch_chamados
                            where (p.id != 0 ? ch.id == p.id : true)
                            where (p.emp != 0 ? ch.emp == p.emp : ch.emp == ch.emp)
                            where (p.statusE != 0 ? ch.statusE == p.statusE : ch.statusE == ch.statusE)
                            where (p.classif != 0 ? ch.classif == p.classif : ch.classif == ch.classif)
                            where (p.user_cli != 0 ? ch.user_cli == p.user_cli : ch.user_cli == ch.user_cli)
                            where (p.user_responsavel != 0 ? ch.user_responsavel == p.user_responsavel : ch.user_responsavel == ch.user_responsavel)
                            where (p.dt_abertura != dt && ate != dt ? ch.dt_abertura >= p.dt_abertura && ch.dt_abertura <= ate : true)
                            select ch   );

 
                .ToList();

            return Buscando.ToList();

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
            catch (Exception ex )
            {
                utl.SetMensagem("false", ex.Message, "danger");
                return utl.GetMensagem();
            }
        }


        #region DropDown
     

        public List<CAD_COLABORADOR> DropDownColab()
        {
            var lista = db.CAD_COLABORADOR.ToList();
            return lista;
        }

        public List<CAD_COLABORADOR> DropDownColaMatrix()
        {
            var lista = db.CAD_COLABORADOR.Where(x => x.emp == 1).ToList();
            return lista;
        }


        #region Drop Busca Com todos os itens em primeiro 

        public List<Generic> DropDownEmpresaTodas()
        {
            List<Generic> lista = (from emp in db.CAD_EMP
                         select new Generic
                         {
                             id = emp.id,
                             descs = emp.descs
                         }).ToList();

            // adicioando empresa
            lista.Add(new Generic
            {
                id = 0,
                descs = "Todas as empresas"
            });

            List<Generic> ListaOrdenada = lista
                .OrderByDescending(x => x.id == 0)
                .ThenBy(x => x.id == 0)
                .ToList();

             
            return ListaOrdenada;
        }


        public List<Generic> DropDownStatusTodos()
        {

            List<Generic> lista = (from tipo in db.ch_status
                         select new Generic
                         {
                             id = tipo.id,
                             descs = tipo.descs
                         }).ToList();

            lista.Add(new Generic
            {
                id = 0,
                descs = "Todos os status"
            });


            List<Generic> ListaOrdenada =  lista
                .OrderByDescending(x => x.id == 0)
                .ThenBy(x => x.id == 0)
                .ToList();

            return ListaOrdenada;

        }


        public List<Generic> DropDownClassificacoTodos()
        {
            List<Generic> lista = (from clasific in db.ch_classificacao
                                   select new Generic
                                   {
                                       id = clasific.id,
                                       descs =  clasific.descs
                                   }).ToList();

            lista.Add(new Generic
            {
                id = 0,
                descs = "Todas as classificações"
            });


            List<Generic> ListaOrdenada = lista
                         .OrderByDescending(x => x.id == 0)
                         .ThenBy(x => x.id == 0)
                         .ToList();

            return ListaOrdenada;
        }


        public List<Generic> DropDownColaboradorTodos()
        {
            List<Generic> lista = (from colab in db.CAD_COLABORADOR
                                   where colab.emp == 1
                                   select new Generic
                                   {
                                       id = colab.id,
                                       descs = colab.nome
                                   }).ToList();

            lista.Add(new Generic
            {
                id = 0,
                descs = "Todos os colaboradores"
            });

            List<Generic> ListaOrdenada = lista
                         .OrderByDescending(x => x.id == 0)
                         .ThenBy(x => x.id == 0)
                         .ToList();

            return ListaOrdenada;
        }

        public List<Generic> SuporteResponsavelTodos()
        {

            List<Generic> lista = (from respo in db.CAD_COLABORADOR
                                   where respo.tipo_u == 1002 || respo.tipo_u == 1003
                                   select new Generic
                                   {
                                       id = respo.id,
                                       descs = respo.nome
                                   }).ToList();

            lista.Add(new Generic
            {
                id = 0,
                descs = "Todos os responsável"
            });

            List<Generic> ListaOrdenada = lista
                         .OrderByDescending(x => x.id == 0)
                         .ThenBy(x => x.id == 0)
                         .ToList();

            return ListaOrdenada;

        }
        #endregion




        public List<CAD_COLABORADOR> SuporteResponsavel()
        {
            var lista = db.CAD_COLABORADOR.Where(x => x.tipo_u == 1002 || x.tipo_u == 1003).ToList();
            return lista;
        }

        public string[] AtribuirTicket(int usuarioResponsavel, int classificacao, int subClassificacao, long id)
        {
            try
            {
                ch_chamados ticket = db.ch_chamados.Where(x => x.id == id).FirstOrDefault();

                if (classificacao != 0 )
                    ticket.classif = classificacao;

                if (subClassificacao != 0)
                ticket.sub_classif = subClassificacao;

                if (usuarioResponsavel != 0)
                   ticket.user_responsavel = usuarioResponsavel;

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                utl.SetMensagem("true", "chamado atribuido com sucesso !", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex )
            {
                utl.SetMensagem("false", ex.Message , "success");
                return utl.GetMensagem();

            }
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
            var lista = db.ch_subclassif.Where(x => x.classifID == 1).ToList();
            return lista;
        }


        public List<ch_subclassif> GetDropDownSubClassificacao(int id)
        {
            var lista = db.ch_subclassif.Where(x => x.classifID == id).ToList();
            return lista;
        }

        public List<CAD_COLABORADOR> UsuarioPorEmpresa(int id)
        {
            var lista = db.CAD_COLABORADOR.Where(x => x.emp == id).ToList();
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

        public string[] AdicionaMensagem(long id , string msg )
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
                    leitura_suporte = "N"
                };

                db.ch_msg.Add(mensaguem);
                db.SaveChanges();
                utl.SetMensagem("true", "Mensagem adicionada com sucesso", "success");
                return utl.GetMensagem();
            }
            catch (Exception ex )
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
                m.leitura_suporte = "S";
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


        public string[] AdicionarAnexos(List<HttpPostedFileBase> postedFile, long Id, string msg)
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

                    Notificao += String.Format("* {0} adicionado com Sucesso", NameFile);

                }
                else
                {
                    Notificao += String.Format("* danger {0} Houve um erro ao adiconar TAMANHO MAX 2MB", NameFile);
                }
            }

            utl.SetMensagem("true", Notificao, "success");
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
                if (c.statusE == 7)
                {
                    Chamado.dt_encerramento = DateTime.Now;
                }

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