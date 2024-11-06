using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestaDinero.Core
{
    public class TablaInteresEntity
    {
        [Key]
        public int IdTablaInteres { get; set; }

        public int IdTipoPrestamo { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "6 Quincenas")]
        public double Q6 { get; set; }

        [Display(Name = "8 Quincenas")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Q8 { get; set; }

        [Display(Name = "10 Quincenas")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Q10 { get; set; }

        [Display(Name = "12 Quincenas")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Q12 { get; set; }

        [Display(Name = "14 Quincenas")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Q14 { get; set; }

        [Display(Name = "16 Quincenas")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Q16 { get; set; }

        [Display(Name = "18 Quincenas")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Q18 { get; set; }

        public bool Activo { get; set; }


            [NotMapped]
            [ForeignKey("IdTipoPrestamo")]
            public virtual TipoPrestamoEntity TipoPrestamo { get; set; }
        }
    

}
