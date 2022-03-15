using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaFact.Models
{
    public enum TipoPago
    {
        Efectivo, Cheque, Tarjeta
    }

    public class Pago
    {
        [Required, Display(Name = "Id Pago")]
        public int ID { get; set; }

        [Required, Display(Name = "Tipo de Pago")]
        public TipoPago? TipoPago { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Valor")]
        public Decimal Valor { get; set; }

        public virtual Factura Factura { get; set; }
    }
}