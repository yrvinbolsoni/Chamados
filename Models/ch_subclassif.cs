namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ch_subclassif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ch_subclassif()
        {
            ch_chamados = new HashSet<ch_chamados>();
        }

        public int id { get; set; }

        public int classifID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Sub-Classifica��o")]
        public string descs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ch_chamados> ch_chamados { get; set; }

        public virtual ch_classificacao ch_classificacao { get; set; }
    }
}
