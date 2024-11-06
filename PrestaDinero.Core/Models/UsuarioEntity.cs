using PrestaDinero.Core.Enumeradores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestaDinero.Core
{
    public class UsuarioEntity
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        public string Correo { get; set; }

        public string Contraseña { get;set; }


        [Required]
        public TipoRolEnum TipoRol { get; set; }
        
        public bool Activo { get; set; }

     
    }
}
