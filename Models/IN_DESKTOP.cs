namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_DESKTOP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_DESKTOP()
        {
            CAD_COLABORADOR = new HashSet<CAD_COLABORADOR>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string identificador { get; set; }

        [StringLength(255)]
        public string ip { get; set; }

        public int emp { get; set; }

        public DateTime? dt_compra { get; set; }

        [StringLength(255)]
        public string k_so { get; set; }

        [StringLength(255)]
        public string k_office { get; set; }

        [StringLength(255)]
        public string hd { get; set; }

        [StringLength(255)]
        public string memoria { get; set; }

        [StringLength(255)]
        public string monitor { get; set; }

        [Required]
        [StringLength(5)]
        public string situacao { get; set; }

        public string info { get; set; }

        public int? monitor_client { get; set; }

        public int? modelo_client { get; set; }

        public int? disco_rigido { get; set; }

        public int? mem_ram { get; set; }

        public int? sis_oper { get; set; }

        public int? pct_office { get; set; }

        public int sit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAD_COLABORADOR> CAD_COLABORADOR { get; set; }

        public virtual CAD_EMP CAD_EMP { get; set; }

        public virtual CAD_ITEM CAD_ITEM { get; set; }

        public virtual CAD_ITEM CAD_ITEM1 { get; set; }

        public virtual CAD_ITEM CAD_ITEM2 { get; set; }

        public virtual CAD_ITEM CAD_ITEM3 { get; set; }

        public virtual CAD_ITEM CAD_ITEM4 { get; set; }

        public virtual CAD_ITEM CAD_ITEM5 { get; set; }

        public virtual cad_Situacao cad_Situacao { get; set; }
    }
}
