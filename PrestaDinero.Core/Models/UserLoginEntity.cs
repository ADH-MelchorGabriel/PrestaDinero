using System.ComponentModel.DataAnnotations;

namespace PrestaDinero.Core
{
    public class UserLoginEntity
    {
        [Required(ErrorMessage = "El {0} es un valor necesario")]
        [StringLength(180, ErrorMessage = "El {0} no cumple con el tamaño requerido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El {0} es un valor necesario")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
