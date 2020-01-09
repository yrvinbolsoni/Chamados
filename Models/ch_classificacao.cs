namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ch_classificacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ch_classificacao()
        {
            ch_chamados = new HashSet<ch_chamados>();
            ch_subclassif = new HashSet<ch_subclassif>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Classifica��o")]
        public string descs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_chamados> ch_chamados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_subclassif> ch_subclassif { get; set; }
    }
}
