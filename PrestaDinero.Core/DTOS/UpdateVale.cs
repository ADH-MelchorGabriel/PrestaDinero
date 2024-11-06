using System;
using System.ComponentModel.DataAnnotations;

namespace PrestaDinero.Core.DTOS
{
    public class UpdateVale
    {
        public int IdVale { get; set; }

        [Display(Name ="Fecha de disposicion")]
        public DateTime FechaDisposicion { get; set; }
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

    }
}
