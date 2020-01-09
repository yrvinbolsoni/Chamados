namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAD_COLABORADOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAD_COLABORADOR()
        {
            ch_chamados = new HashSet<ch_chamados>();
            ch_chamados1 = new HashSet<ch_chamados>();
            ch_msg = new HashSet<ch_msg>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Cliente")]
        public string nome { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("User Name")]
        public string username { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Password")]
        public string passwd { get; set; }

        [DisplayName("Departamento")]
        public int? dept { get; set; }

        [DisplayName("Empresa")]
        public int? emp { get; set; }

        [DisplayName("Ramal")]
        public int? ramal { get; set; }

        [DisplayName("DeskTop")]
        public int? desktop { get; set; }

        [DisplayName("TipoUsuario")]
        public int? tipo_u { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("E-mail")]
        public string email { get; set; }

        [Required]
        [StringLength(1)]
        [DisplayName("Status")]
        public string STATUS { get; set; }

        public long? SMARTPHONE { get; set; }

        public virtual IN_DESKTOP IN_DESKTOP { get; set; }

        public virtual IN_VOIP IN_VOIP { get; set; }

        public virtual CAD_TIPO_USER CAD_TIPO_USER { get; set; }

        public virtual CAD_DEPT CAD_DEPT { get; set; }

        public virtual CAD_EMP CAD_EMP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_chamados> ch_chamados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_chamados> ch_chamados1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_msg> ch_msg { get; set; }

        public virtual IN_SMARTPHONE IN_SMARTPHONE { get; set; }
    }
}
