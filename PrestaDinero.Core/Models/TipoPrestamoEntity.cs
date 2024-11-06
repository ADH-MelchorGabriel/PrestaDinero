using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestaDinero.Core
{
    public class TipoPrestamoEntity
    {
        [Key]
        public int IdTipoPrestamo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public bool Activo { get; set; }




        [NotMapped]
        public virtual List<ValeEntity> Vales{ get; set; } 



    }
}
