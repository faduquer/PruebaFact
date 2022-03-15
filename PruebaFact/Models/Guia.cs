using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaFact.Models
{
    public class Guia
    {
        [Required, Display(Name = "Id Guía")]
        public int ID { get; set; }

        [Required, StringLength(10), Display(Name = "Nro Guía")]
        public string NumeroGuía { get; set; }
        
        [Required, DataType(DataType.Date), Display(Name = "Fecha de envío")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaEnvio { get; set; }

        [Required, StringLength(100), Display(Name = "País de origen")]
        public string PaisOrigen { get; set; }

        [Required, StringLength(100), Display(Name = "Nombre Remitente")]
        [RegularExpression(@"^[A-Z a-z0-9ÑñáéíóúÁÉÍÓÚ\\-\\_]+$", ErrorMessage = "El Nombre debe ser alfanumérico.")]
        public string NombreRemitente { get; set; }

        [Required, StringLength(100), Display(Name = "Dirección Remitente")]
        public string DireccionRemitente { get; set; }

        [StringLength(50), Display(Name = "Teléfono Remitente")]
        public string TelefonoRemitente { get; set; }

        [StringLength(50), Display(Name = "Email Remitente")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        public string EmailRemitente { get; set; }

        [Required, StringLength(100), Display(Name = "País de destino")]
        public string PaisDestino { get; set; }

        [Required, StringLength(100), Display(Name = "Nombre Destinatario")]
        [RegularExpression(@"^[A-Z a-z0-9ÑñáéíóúÁÉÍÓÚ\\-\\_]+$", ErrorMessage = "El Nombre debe ser alfanumérico.")]
        public string NombreDestinatario { get; set; }

        [Required, StringLength(100), Display(Name = "Dirección Destinatario")]
        public string DireccionDestinatario { get; set; }

        [StringLength(50), Display(Name = "Teléfono Destinatario")]
        public string TelefonoDestinatario { get; set; }

        [StringLength(50), Display(Name = "Email Destinatario")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        public string EmailDestinatario { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = "Total")]
        public Decimal Total { get; set; }

        public virtual Factura Factura { get; set; }

    }
}