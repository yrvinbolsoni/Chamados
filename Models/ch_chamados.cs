namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ch_chamados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ch_chamados()
        {
            ch_anexo = new HashSet<ch_anexo>();
            ch_msg = new HashSet<ch_msg>();
        }
        [DisplayName("Nro")]
        public long id { get; set; }
        [DisplayName("Empresa")]
        public int emp { get; set; }
        [DisplayName("Cliente")]
        public int user_cli { get; set; }
        [DisplayName("Prioridade")]
        public int? prioridade { get; set; }

        [DisplayName("Status")]
        public int? statusE { get; set; }

        [DisplayName("Tipo")]
        public int? tipo { get; set; }

        [DisplayName("Classificação")]
        public int? classif { get; set; }

        [DisplayName("Sub-Classificação")]
        public int? sub_classif { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Assunto")]
        public string assunto { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string descricacao { get; set; }

        [StringLength(255)]
        [DisplayName("Com Copia")]
        public string com_copia { get; set; }

        [DisplayName("Responsavel")]
        public int? user_responsavel { get; set; }

        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_abertura { get; set; }

        [DisplayName("Data Encerramento")]
        public DateTime? dt_encerramento { get; set; }

        public virtual CAD_COLABORADOR CAD_COLABORADOR { get; set; }

        public virtual CAD_COLABORADOR CAD_COLABORADOR1 { get; set; }

        public virtual CAD_EMP CAD_EMP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_anexo> ch_anexo { get; set; }

        public virtual ch_classificacao ch_classificacao { get; set; }

        public virtual ch_prioridade ch_prioridade { get; set; }

        public virtual ch_status ch_status { get; set; }

        public virtual ch_subclassif ch_subclassif { get; set; }

        public virtual ch_tipo ch_tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_msg> ch_msg { get; set; }
    }
}
