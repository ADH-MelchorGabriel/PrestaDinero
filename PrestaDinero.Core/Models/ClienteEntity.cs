using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestaDinero.Core
{
    public  class ClienteEntity
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        public string ApellidoMaterno { get; set; }


        public string Correo { get; set; } = null;

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = null;
        
        [Required]
        [Display(Name = "Limite de crédito")]
        public double LimiteCredito { get; set; }

       

        public bool Activo { get; set; }


        [NotMapped]
        [Display(Name = "Nombre")]
        public string NombreCompleto { get { return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}"; } }

        


        [NotMapped]
        public virtual List<ValeEntity> Vales { get; set; }

    }
}
