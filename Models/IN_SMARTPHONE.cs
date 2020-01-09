namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_SMARTPHONE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_SMARTPHONE()
        {
            CAD_COLABORADOR = new HashSet<CAD_COLABORADOR>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string serial_number { get; set; }

        public int model { get; set; }

        [Required]
        [StringLength(255)]
        public string imei { get; set; }

        [StringLength(255)]
        public string imei2 { get; set; }

        public DateTime DATA_COMPRA { get; set; }

        public int EMP { get; set; }

        [StringLength(50)]
        public string TERM_NAME { get; set; }

        [StringLength(50)]
        public string TERM_TYPE { get; set; }

        public byte[] TERM_FILE { get; set; }

        public long? linha_movel { get; set; }

        public long? linha_movel2 { get; set; }

        [StringLength(255)]
        public string info { get; set; }

        public int situacao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_COLABORADOR> CAD_COLABORADOR { get; set; }

        public virtual CAD_EMP CAD_EMP { get; set; }

        public virtual CAD_ITEM CAD_ITEM { get; set; }

        public virtual cad_Situacao cad_Situacao { get; set; }

        public virtual IN_LINHA_MOVEL IN_LINHA_MOVEL { get; set; }

        public virtual IN_LINHA_MOVEL IN_LINHA_MOVEL1 { get; set; }
    }
}
