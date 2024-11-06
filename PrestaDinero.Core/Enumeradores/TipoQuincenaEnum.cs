using System.ComponentModel.DataAnnotations;

namespace PrestaDinero.Core.Enumeradores
{
    public enum TipoQuincenaEnum
    {
        [Display(Name ="Primera quincena")]
        PrimeraQuicena =15,
        [Display(Name = "Segunda quincena")]
        SegundaQuicena =28      
    }
}
