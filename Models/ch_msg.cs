namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ch_msg
    {
        public long id { get; set; }

        public long? id_chamado { get; set; }

        public int? id_user { get; set; }

        [Required]
        [StringLength(1)]
        public string leitura_cli { get; set; }

        [StringLength(1)]
        public string leitura_suporte { get; set; }

        public string msg { get; set; }

        public DateTime dt_post { get; set; }

        public virtual CAD_COLABORADOR CAD_COLABORADOR { get; set; }

        public virtual ch_chamados ch_chamados { get; set; }
    }
}
