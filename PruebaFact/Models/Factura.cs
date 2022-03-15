using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PruebaFact.Models
{
    public class Factura
    {
        [Required, Display(Name = "Id Factura")]
        public int ID { get; set; }

        [Required(ErrorMessage = "*"), StringLength(3), Display(Name = "Establecimiento")]
        public string Establecimiento { get; set; }

        [Required, StringLength(3), Display(Name = "Punto Emisión")]
        public string PuntoEmision { get; set; }

        [Required, Display(Name = "Secuencial")]
        public int Secuencial { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Emisión")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaEmision { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Sub-total")]
        public Decimal Subtotal { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Impuesto")]
        public Decimal Impuesto { get; set; }

        [DataType(DataType.Currency), Display(Name = "Total")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Decimal Total { get; set; }

        [Display(Name = "Guías")]
        public int GuiasAsocioadas { 
            get { return Guias == null ? 0: Guias.Count(); }
        }

        [Display(Name = "Pagos")]
        public int PagosAsocioados
        {
            get { return Pagos == null ? 0 : Pagos.Count(); }
        }

        public virtual ICollection<Guia> Guias { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }

    }
}