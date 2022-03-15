using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaFact.ViewModels
{
    public class GuiaAsignadaData
    {
        public int ID { get; set; }
        public string NumeroGuía { get; set; }

        public DateTime FechaEnvio { get; set; }
        public Decimal Total { get; set; }
        public bool Asignada { get; set; }
    }
}