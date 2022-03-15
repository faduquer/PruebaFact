using System;
using PruebaFact.Models;

namespace PruebaFact.ViewModels
{
    public class PagoAsignadoData
    {
        public int ID { get; set; }
        public TipoPago?  TipoPago { get; set; }
        public Decimal Valor { get; set; }
        public bool Asignado { get; set; }
    }
}