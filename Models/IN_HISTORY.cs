namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_HISTORY
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string identificador { get; set; }

        [Required]
        [StringLength(255)]
        public string descs { get; set; }

        [Required]
        [StringLength(255)]
        public string tipo { get; set; }

        public DateTime dt { get; set; }

        public int TYPE_ITEM { get; set; }

        public virtual CAD_ITEM_TYPE CAD_ITEM_TYPE { get; set; }
    }
}
