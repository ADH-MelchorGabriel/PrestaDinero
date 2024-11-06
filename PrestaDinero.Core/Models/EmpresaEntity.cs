using System.ComponentModel.DataAnnotations;

namespace PrestaDinero.Core
{
    public class EmpresaEntity
    {
        [Key]
        public int IdEmpresa { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}
