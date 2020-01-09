namespace Chamados.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbChamados : DbContext
    {
        public DbChamados()
            : base("name=DbChamados")
        {
        }

        public virtual DbSet<CAD_COLABORADOR> CAD_COLABORADOR { get; set; }
        public virtual DbSet<CAD_DEPT> CAD_DEPT { get; set; }
        public virtual DbSet<CAD_EMP> CAD_EMP { get; set; }
        public virtual DbSet<CAD_ITEM> CAD_ITEM { get; set; }
        public virtual DbSet<CAD_ITEM_TYPE> CAD_ITEM_TYPE { get; set; }
        public virtual DbSet<CAD_LOCAL> CAD_LOCAL { get; set; }
        public virtual DbSet<cad_Situacao> cad_Situacao { get; set; }
        public virtual DbSet<CAD_TIPO_USER> CAD_TIPO_USER { get; set; }
        public virtual DbSet<ch_anexo> ch_anexo { get; set; }
        public virtual DbSet<ch_chamados> ch_chamados { get; set; }
        public virtual DbSet<ch_classificacao> ch_classificacao { get; set; }
        public virtual DbSet<ch_msg> ch_msg { get; set; }
        public virtual DbSet<ch_prioridade> ch_prioridade { get; set; }
        public virtual DbSet<ch_status> ch_status { get; set; }
        public virtual DbSet<ch_subclassif> ch_subclassif { get; set; }
        public virtual DbSet<ch_tipo> ch_tipo { get; set; }
        public virtual DbSet<IN_DESKTOP> IN_DESKTOP { get; set; }
        public virtual DbSet<IN_HISTORY> IN_HISTORY { get; set; }
        public virtual DbSet<IN_LINHA_MOVEL> IN_LINHA_MOVEL { get; set; }
        public virtual DbSet<IN_PRINTER> IN_PRINTER { get; set; }
        public virtual DbSet<IN_SMARTPHONE> IN_SMARTPHONE { get; set; }
        public virtual DbSet<IN_VOIP> IN_VOIP { get; set; }
        public virtual DbSet<NFE> NFE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CAD_COLABORADOR>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .Property(e => e.passwd)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .HasMany(e => e.ch_chamados)
                .WithRequired(e => e.CAD_COLABORADOR)
                .HasForeignKey(e => e.user_cli)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .HasMany(e => e.ch_chamados1)
                .WithOptional(e => e.CAD_COLABORADOR1)
                .HasForeignKey(e => e.user_responsavel);

            modelBuilder.Entity<CAD_COLABORADOR>()
                .HasMany(e => e.ch_msg)
                .WithOptional(e => e.CAD_COLABORADOR)
                .HasForeignKey(e => e.id_user);

            modelBuilder.Entity<CAD_DEPT>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_DEPT>()
                .HasMany(e => e.CAD_COLABORADOR)
                .WithOptional(e => e.CAD_DEPT)
                .HasForeignKey(e => e.dept);

            modelBuilder.Entity<CAD_DEPT>()
                .HasMany(e => e.IN_PRINTER)
                .WithOptional(e => e.CAD_DEPT)
                .HasForeignKey(e => e.DEPTID);

            modelBuilder.Entity<CAD_EMP>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_EMP>()
                .Property(e => e.cnpj)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_EMP>()
                .Property(e => e.endereco)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_EMP>()
                .Property(e => e.telefone)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_EMP>()
                .Property(e => e.cep)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.CAD_COLABORADOR)
                .WithOptional(e => e.CAD_EMP)
                .HasForeignKey(e => e.emp);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.CAD_DEPT)
                .WithOptional(e => e.CAD_EMP)
                .HasForeignKey(e => e.emp);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.ch_chamados)
                .WithRequired(e => e.CAD_EMP)
                .HasForeignKey(e => e.emp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.IN_DESKTOP)
                .WithRequired(e => e.CAD_EMP)
                .HasForeignKey(e => e.emp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.IN_LINHA_MOVEL)
                .WithRequired(e => e.CAD_EMP)
                .HasForeignKey(e => e.EMPID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.IN_PRINTER)
                .WithRequired(e => e.CAD_EMP)
                .HasForeignKey(e => e.EMPID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.IN_SMARTPHONE)
                .WithRequired(e => e.CAD_EMP)
                .HasForeignKey(e => e.EMP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.IN_VOIP)
                .WithRequired(e => e.CAD_EMP)
                .HasForeignKey(e => e.emp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_EMP>()
                .HasMany(e => e.NFE)
                .WithOptional(e => e.CAD_EMP)
                .HasForeignKey(e => e.emp);

            modelBuilder.Entity<CAD_ITEM>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_PRINTER)
                .WithRequired(e => e.CAD_ITEM)
                .HasForeignKey(e => e.MODEL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_DESKTOP)
                .WithOptional(e => e.CAD_ITEM)
                .HasForeignKey(e => e.monitor_client);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_DESKTOP1)
                .WithOptional(e => e.CAD_ITEM1)
                .HasForeignKey(e => e.modelo_client);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_DESKTOP2)
                .WithOptional(e => e.CAD_ITEM2)
                .HasForeignKey(e => e.disco_rigido);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_DESKTOP3)
                .WithOptional(e => e.CAD_ITEM3)
                .HasForeignKey(e => e.mem_ram);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_DESKTOP4)
                .WithOptional(e => e.CAD_ITEM4)
                .HasForeignKey(e => e.sis_oper);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_DESKTOP5)
                .WithOptional(e => e.CAD_ITEM5)
                .HasForeignKey(e => e.pct_office);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_SMARTPHONE)
                .WithRequired(e => e.CAD_ITEM)
                .HasForeignKey(e => e.model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_ITEM>()
                .HasMany(e => e.IN_VOIP)
                .WithRequired(e => e.CAD_ITEM)
                .HasForeignKey(e => e.modelo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_ITEM_TYPE>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_ITEM_TYPE>()
                .Property(e => e.ITEM_TP)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_ITEM_TYPE>()
                .HasMany(e => e.CAD_ITEM)
                .WithOptional(e => e.CAD_ITEM_TYPE)
                .HasForeignKey(e => e.typeID);

            modelBuilder.Entity<CAD_ITEM_TYPE>()
                .HasMany(e => e.IN_HISTORY)
                .WithRequired(e => e.CAD_ITEM_TYPE)
                .HasForeignKey(e => e.TYPE_ITEM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_ITEM_TYPE>()
                .HasMany(e => e.NFE)
                .WithRequired(e => e.CAD_ITEM_TYPE)
                .HasForeignKey(e => e.TYPE_ITEM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_LOCAL>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_LOCAL>()
                .HasMany(e => e.CAD_EMP)
                .WithRequired(e => e.CAD_LOCAL)
                .HasForeignKey(e => e.local_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cad_Situacao>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<cad_Situacao>()
                .HasMany(e => e.IN_DESKTOP)
                .WithRequired(e => e.cad_Situacao)
                .HasForeignKey(e => e.sit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cad_Situacao>()
                .HasMany(e => e.IN_LINHA_MOVEL)
                .WithRequired(e => e.cad_Situacao)
                .HasForeignKey(e => e.situacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cad_Situacao>()
                .HasMany(e => e.IN_PRINTER)
                .WithRequired(e => e.cad_Situacao)
                .HasForeignKey(e => e.situacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cad_Situacao>()
                .HasMany(e => e.IN_SMARTPHONE)
                .WithRequired(e => e.cad_Situacao)
                .HasForeignKey(e => e.situacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cad_Situacao>()
                .HasMany(e => e.IN_VOIP)
                .WithRequired(e => e.cad_Situacao)
                .HasForeignKey(e => e.situacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAD_TIPO_USER>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<CAD_TIPO_USER>()
                .HasMany(e => e.CAD_COLABORADOR)
                .WithOptional(e => e.CAD_TIPO_USER)
                .HasForeignKey(e => e.tipo_u);

            modelBuilder.Entity<ch_anexo>()
                .Property(e => e.a_name)
                .IsUnicode(false);

            modelBuilder.Entity<ch_anexo>()
                .Property(e => e.a_type)
                .IsUnicode(false);

            modelBuilder.Entity<ch_chamados>()
                .Property(e => e.assunto)
                .IsUnicode(false);

            modelBuilder.Entity<ch_chamados>()
                .Property(e => e.descricacao)
                .IsUnicode(false);

            modelBuilder.Entity<ch_chamados>()
                .Property(e => e.com_copia)
                .IsUnicode(false);

            modelBuilder.Entity<ch_chamados>()
                .HasMany(e => e.ch_anexo)
                .WithRequired(e => e.ch_chamados)
                .HasForeignKey(e => e.id_chamado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ch_chamados>()
                .HasMany(e => e.ch_msg)
                .WithOptional(e => e.ch_chamados)
                .HasForeignKey(e => e.id_chamado);

            modelBuilder.Entity<ch_classificacao>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<ch_classificacao>()
                .HasMany(e => e.ch_chamados)
                .WithOptional(e => e.ch_classificacao)
                .HasForeignKey(e => e.classif);

            modelBuilder.Entity<ch_classificacao>()
                .HasMany(e => e.ch_subclassif)
                .WithRequired(e => e.ch_classificacao)
                .HasForeignKey(e => e.classifID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ch_msg>()
                .Property(e => e.leitura_cli)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ch_prioridade>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<ch_prioridade>()
                .HasMany(e => e.ch_chamados)
                .WithOptional(e => e.ch_prioridade)
                .HasForeignKey(e => e.prioridade);

            modelBuilder.Entity<ch_status>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<ch_status>()
                .HasMany(e => e.ch_chamados)
                .WithOptional(e => e.ch_status)
                .HasForeignKey(e => e.statusE);

            modelBuilder.Entity<ch_subclassif>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<ch_subclassif>()
                .HasMany(e => e.ch_chamados)
                .WithOptional(e => e.ch_subclassif)
                .HasForeignKey(e => e.sub_classif);

            modelBuilder.Entity<ch_tipo>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<ch_tipo>()
                .HasMany(e => e.ch_chamados)
                .WithOptional(e => e.ch_tipo)
                .HasForeignKey(e => e.tipo);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.identificador)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.k_so)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.k_office)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.hd)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.memoria)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.monitor)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.situacao)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .Property(e => e.info)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DESKTOP>()
                .HasMany(e => e.CAD_COLABORADOR)
                .WithOptional(e => e.IN_DESKTOP)
                .HasForeignKey(e => e.desktop);

            modelBuilder.Entity<IN_HISTORY>()
                .Property(e => e.identificador)
                .IsUnicode(false);

            modelBuilder.Entity<IN_HISTORY>()
                .Property(e => e.descs)
                .IsUnicode(false);

            modelBuilder.Entity<IN_HISTORY>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<IN_LINHA_MOVEL>()
                .Property(e => e.DESCS)
                .IsUnicode(false);

            modelBuilder.Entity<IN_LINHA_MOVEL>()
                .Property(e => e.ICCID)
                .IsUnicode(false);

            modelBuilder.Entity<IN_LINHA_MOVEL>()
                .Property(e => e.tipo_plano)
                .IsUnicode(false);

            modelBuilder.Entity<IN_LINHA_MOVEL>()
                .Property(e => e.custo_aparelho_plano)
                .IsUnicode(false);

            modelBuilder.Entity<IN_LINHA_MOVEL>()
                .HasMany(e => e.IN_SMARTPHONE)
                .WithOptional(e => e.IN_LINHA_MOVEL)
                .HasForeignKey(e => e.linha_movel);

            modelBuilder.Entity<IN_LINHA_MOVEL>()
                .HasMany(e => e.IN_SMARTPHONE1)
                .WithOptional(e => e.IN_LINHA_MOVEL1)
                .HasForeignKey(e => e.linha_movel2);

            modelBuilder.Entity<IN_PRINTER>()
                .Property(e => e.SERIAL_NO)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRINTER>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRINTER>()
                .Property(e => e.APELIDO)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRINTER>()
                .Property(e => e.info)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .Property(e => e.serial_number)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .Property(e => e.imei)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .Property(e => e.imei2)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .Property(e => e.TERM_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .Property(e => e.TERM_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .Property(e => e.info)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SMARTPHONE>()
                .HasMany(e => e.CAD_COLABORADOR)
                .WithOptional(e => e.IN_SMARTPHONE)
                .HasForeignKey(e => e.SMARTPHONE);

            modelBuilder.Entity<IN_VOIP>()
                .Property(e => e.passwd)
                .IsUnicode(false);

            modelBuilder.Entity<IN_VOIP>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<IN_VOIP>()
                .Property(e => e.INFO)
                .IsUnicode(false);

            modelBuilder.Entity<NFE>()
                .Property(e => e.identificador)
                .IsUnicode(false);

            modelBuilder.Entity<NFE>()
                .Property(e => e.n_name)
                .IsUnicode(false);

            modelBuilder.Entity<NFE>()
                .Property(e => e.n_type)
                .IsUnicode(false);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);    
        }
    }
}
