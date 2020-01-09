namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ch_anexo
    {
        public long id { get; set; }

        public long id_chamado { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Nome")]
        public string a_name { get; set; }

        [Required]
        [StringLength(255)]
        public string a_type { get; set; }

        [Required]
        public byte[] a_file { get; set; }

        public DateTime dt_insert { get; set; }

        public virtual ch_chamados ch_chamados { get; set; }
    }
}
