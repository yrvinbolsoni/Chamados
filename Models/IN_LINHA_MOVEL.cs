namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_LINHA_MOVEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_LINHA_MOVEL()
        {
            IN_SMARTPHONE = new HashSet<IN_SMARTPHONE>();
            IN_SMARTPHONE1 = new HashSet<IN_SMARTPHONE>();
        }

        public long ID { get; set; }

        public int EMPID { get; set; }

        [Required]
        [StringLength(50)]
        public string DESCS { get; set; }

        [StringLength(100)]
        public string ICCID { get; set; }

        [StringLength(255)]
        public string tipo_plano { get; set; }

        public int situacao { get; set; }

        [StringLength(255)]
        public string custo_aparelho_plano { get; set; }

        public virtual CAD_EMP CAD_EMP { get; set; }

        public virtual cad_Situacao cad_Situacao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_SMARTPHONE> IN_SMARTPHONE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_SMARTPHONE> IN_SMARTPHONE1 { get; set; }
    }
}
