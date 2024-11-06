using System.ComponentModel.DataAnnotations;

namespace PrestaDinero.Core.Enumeradores
{
    public enum QuincenasEnum
    {
        [Display(Name = "6 Quincenas")] Q6 = 6,
        [Display(Name = "8 Quincenas")] Q8 =8,
        [Display(Name = "10 Quincenas")] Q10 =10,
        [Display(Name = "12 Quincenas")] Q12 =12,
        [Display(Name = "14 Quincenas")] Q14 =14,
        [Display(Name = "16 Quincenas")] Q16 = 16,
        [Display(Name = "18 Quincenas")] Q18 = 18
    }
}
