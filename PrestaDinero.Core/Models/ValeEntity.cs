using PrestaDinero.Core.Enumeradores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PrestaDinero.Core
{
    public class ValeEntity
    {
        [Key]
        [Display(Name = "Folio")]
        public int IdVale { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de disposición")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name ="Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Distribuidor")]
        public int IdDistribuidor { get; set; }

        [Required]
        [Display(Name = "Tipo de prestamo")]
        public int IdTipoPrestamo { get; set; }

        [Required]
        public QuincenasEnum Quincenas { get; set; }

        [Required]
        public bool FechaPorConvenio { get; set; }


        [Required]
        [Display(Name = "Fecha de primer pago")]
        public DateTime FechaPrimerPago { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Disposición")]
        public double Dispocision { get; set; }

        
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Interés")]

        public double Interes{ get; set; }



        public bool EstaPagado { get; set; }= false;


        [NotMapped]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Total { get { return Dispocision + Interes ; } }

        [NotMapped]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Saldo { get { 
                if(ValeDetalle.Count <=0 )
                {
                    return 0;
                }
                
                return Total- (ValeDetalle.FirstOrDefault().Abono * ValeDetalle.FirstOrDefault().Parcialidad); } }

        [NotMapped]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double SaldoAnterior { get {  if (ValeDetalle.Count == 0)
                {
                    return 0;
                }
        else
            return        Total - (ValeDetalle.FirstOrDefault().Abono * (ValeDetalle.FirstOrDefault().Parcialidad-1)); } }// Total - (ValeDetalle.FirstOrDefault().Abono * (ValeDetalle.FirstOrDefault().Parcialidad-1)); } }

        [NotMapped]
        [ForeignKey("IdCliente")]
        public virtual ClienteEntity Cliente { get; set; }

        [NotMapped]
        [ForeignKey("IdDistribuidor")]
        public virtual DistribuidorEntity Distribuidor { get; set; }


        [NotMapped]
        [ForeignKey("IdTipoPrestamo")]
        public virtual TipoPrestamoEntity TipoPrestamo { get; set; }


        [NotMapped]
        public virtual List<ValeDetalleEntity> ValeDetalle { get; set; } = new List<ValeDetalleEntity>();

    }
}
