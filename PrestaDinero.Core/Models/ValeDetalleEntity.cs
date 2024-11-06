using PrestaDinero.Core.Enumeradores;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestaDinero.Core
{
    public class ValeDetalleEntity
    {
        [Key]
        public int IdvaleDetalle { get; set; }

        [Required]
        [Display(Name = "Folio")]
        public int IdVale { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        public int Parcialidad { get; set; }

        [Required]

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Dispocision { get; set; }

        [Required]

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Interés")]
        public double Interes { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Saldo anterior")]
        public double SaldoAnterior { get; set; }

        [Required]
        public EstatusValeEnum Estatus { get; set; }

        [NotMapped]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Abono => Dispocision + Interes;



        [NotMapped]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Comision { get { return Abono * .18; } }

        [NotMapped]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Total { get { return Abono-Comision; } }


        [NotMapped]
        public virtual ValeEntity  Vale { get; set; }


        [NotMapped]
        [Display(Name ="No. Pago")]
        public string NumeroPago { get { return $"{Parcialidad}/{ (int)Vale.Quincenas}"  ; } }
        
      

    }
}
