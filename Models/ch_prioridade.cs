namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ch_prioridade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ch_prioridade()
        {
            ch_chamados = new HashSet<ch_chamados>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Prioridade")]
        public string descs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_chamados> ch_chamados { get; set; }
    }
}
