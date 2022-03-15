using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PruebaFact.Models;

namespace PruebaFact.DAL
{
    public class FacturaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FacturaContext>
    {
        protected override void Seed(FacturaContext context)
        {
            var facturas = new List<Factura>
            {
                new Factura{ Establecimiento="Es1", PuntoEmision="Pn1", Secuencial=1, FechaEmision=DateTime.Parse("2022-03-13"), Subtotal=152.15M, Impuesto=18.26M, Total=170.41M },
                new Factura{ Establecimiento="Es2", PuntoEmision="Pn2", Secuencial=1, FechaEmision=DateTime.Parse("2022-03-14"), Subtotal=250.23M, Impuesto=30.03M, Total=280.26M },
            };
            facturas.ForEach(s => context.Facturas.Add(s));
            context.SaveChanges();


            Factura fact1 = context.Facturas.Find(1);
            Factura fact2 = context.Facturas.Find(2);


            var pagos = new List<Pago>
            {
            new Pago{ TipoPago=TipoPago.Cheque, Valor=(Decimal)98.26, Factura=fact1 },
            new Pago{ TipoPago=TipoPago.Efectivo, Valor=(Decimal)72.15, Factura=fact1 },
            new Pago{ TipoPago=TipoPago.Cheque, Valor=(Decimal)102, Factura=fact2 },
            new Pago{ TipoPago=TipoPago.Efectivo, Valor=(Decimal)72.34, Factura=fact2 },
            new Pago{ TipoPago=TipoPago.Tarjeta, Valor=(Decimal)105.92, Factura=fact2 },
            new Pago{ TipoPago=TipoPago.Tarjeta, Valor=(Decimal)99.42 },
            new Pago{ TipoPago=TipoPago.Tarjeta, Valor=(Decimal)789.01 },
            new Pago{ TipoPago=TipoPago.Tarjeta, Valor=(Decimal)450.10 }
            };
            pagos.ForEach(s => context.Pagos.Add(s));
            context.SaveChanges();

            var guias = new List<Guia>
            {
            new Guia{NumeroGuía = "101010", FechaEnvio=DateTime.Parse("2022-02-06"), PaisOrigen="Ecuador1", NombreRemitente="Franklin1", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)25.15, Factura=fact1 },
            new Guia{NumeroGuía = "101011", FechaEnvio=DateTime.Parse("2022-02-07"), PaisOrigen="Ecuador2", NombreRemitente="Franklin2", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)21.1, Factura=fact1 },
            new Guia{NumeroGuía = "101012", FechaEnvio=DateTime.Parse("2022-02-07"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)30, Factura=fact1 },
            new Guia{NumeroGuía = "101013", FechaEnvio=DateTime.Parse("2022-02-07"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)23.41, Factura=fact1 },
            new Guia{NumeroGuía = "101014", FechaEnvio=DateTime.Parse("2022-02-08"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)45, Factura=fact1 },
            new Guia{NumeroGuía = "101015", FechaEnvio=DateTime.Parse("2022-02-08"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)7.49, Factura=fact1 },
            new Guia{NumeroGuía = "101016", FechaEnvio=DateTime.Parse("2022-02-20"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)15.45 },
            new Guia{NumeroGuía = "101017", FechaEnvio=DateTime.Parse("2022-02-21"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)99.42 },
            new Guia{NumeroGuía = "101018", FechaEnvio=DateTime.Parse("2022-02-28"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)789.01 },
            new Guia{NumeroGuía = "101019", FechaEnvio=DateTime.Parse("2022-03-01"), PaisOrigen="Ecuador3", NombreRemitente="Franklin3", DireccionRemitente="Dir Franklin",TelefonoRemitente="04127139700",EmailRemitente="Franklin@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)450.10 },

            new Guia{NumeroGuía = "109720", FechaEnvio=DateTime.Parse("2022-03-01"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)79, Factura=fact2 },
            new Guia{NumeroGuía = "109727", FechaEnvio=DateTime.Parse("2022-03-01"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)15, Factura=fact2 },
            new Guia{NumeroGuía = "109730", FechaEnvio=DateTime.Parse("2022-03-02"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)55, Factura=fact2 },
            new Guia{NumeroGuía = "109731", FechaEnvio=DateTime.Parse("2022-03-07"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)15.12, Factura=fact2 },
            new Guia{NumeroGuía = "109740", FechaEnvio=DateTime.Parse("2022-03-10"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)30, Factura=fact2 },
            new Guia{NumeroGuía = "109748", FechaEnvio=DateTime.Parse("2022-03-11"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)47.7, Factura=fact2 },
            new Guia{NumeroGuía = "109760", FechaEnvio=DateTime.Parse("2022-03-12"), PaisOrigen="Ecuador1", NombreRemitente="Alfredo1", DireccionRemitente="Dir Alfredo1",TelefonoRemitente="04127139700",EmailRemitente="Alfredo@aa.com",PaisDestino="Ecuador",NombreDestinatario="Franklin",DireccionDestinatario="Dir Franklin",TelefonoDestinatario="04127139700",EmailDestinatario="Franklin@aa.com",Total=(Decimal)8.41, Factura=fact2 },
            };
            guias.ForEach(s => context.Guias.Add(s));
            context.SaveChanges();
        }
    }
}