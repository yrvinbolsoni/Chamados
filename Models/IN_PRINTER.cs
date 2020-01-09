namespace Chamados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_PRINTER
    {
        public long ID { get; set; }

        [Required]
        [StringLength(255)]
        public string SERIAL_NO { get; set; }

        public int MODEL { get; set; }

        public int EMPID { get; set; }

        public int? DEPTID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        [StringLength(50)]
        public string APELIDO { get; set; }

        [StringLength(255)]
        public string info { get; set; }

        public int situacao { get; set; }

        public virtual CAD_DEPT CAD_DEPT { get; set; }

        public virtual CAD_EMP CAD_EMP { get; set; }

        public virtual CAD_ITEM CAD_ITEM { get; set; }

        public virtual cad_Situacao cad_Situacao { get; set; }
    }
}
