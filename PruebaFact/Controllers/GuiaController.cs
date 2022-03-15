using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PruebaFact.DAL;
using PruebaFact.Models;
using PagedList;
using static PruebaFact.Models.EnumAlert;

namespace PruebaFact.Controllers
{
    public class GuiaController : BaseController
    {
        private FacturaContext db = new FacturaContext();

        // GET: Guia
        public ActionResult Index(
            string sortOrder, 
            string actgualNumeroGuia, 
            string busquedaNumeroGuia, 
            string actgualIdFactura, 
            string busquedaIdFactura, 
            int? page)
        {

            if (sortOrder == null)
                sortOrder = "FechaEnvio_desc";

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumeroGuiaSortParm = String.IsNullOrEmpty(sortOrder) ? "NumeroGuia_desc" : "";
            ViewBag.FechaEnvioSortParm = sortOrder == "FechaEnvio" ? "FechaEnvio_desc" : "FechaEnvio";
            ViewBag.IdFacturaSortParm = sortOrder == "IdFactura" ? "IdFactura_desc" : "IdFactura";

            if (busquedaNumeroGuia != null || busquedaIdFactura != null)
            {
                page = 1;
            }
            else
            {
                busquedaNumeroGuia = actgualNumeroGuia;
                busquedaIdFactura = actgualIdFactura;
            }

            ViewBag.actgualNumeroGuia = busquedaNumeroGuia;
            ViewBag.actgualNumeroFactura = busquedaIdFactura;

            var guias = from s in db.Guias
                           select s;

            if (!String.IsNullOrEmpty(busquedaNumeroGuia))
                guias = guias.Where(s => s.NumeroGuía.Contains(busquedaNumeroGuia));

            if (!String.IsNullOrEmpty(busquedaIdFactura))
                guias = guias.Where(s => s.Factura.ID.ToString().Contains(busquedaIdFactura));

            switch (sortOrder)
            {
                case "NumeroGuia_desc":
                    guias = guias.OrderByDescending(s => s.NumeroGuía);
                    break;
                case "FechaEnvio":
                    guias = guias.OrderBy(s => s.FechaEnvio);
                    break;
                case "FechaEnvio_desc":
                    guias = guias.OrderByDescending(s => s.FechaEnvio);
                    break;
                case "IdFactura":
                    guias = guias.OrderBy(s => s.Factura.ID);
                    break;
                case "IdFactura_desc":
                    guias = guias.OrderByDescending(s => s.Factura.ID);
                    break;
                default:
                    guias = guias.OrderBy(s => s.NumeroGuía);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(guias.ToPagedList(pageNumber, pageSize));
        }

        // GET: Guia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guia guia = db.Guias.Find(id);
            if (guia == null)
            {
                return HttpNotFound();
            }
            return View(guia);
        }

        // GET: Guia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumeroGuía,FechaEnvio,PaisOrigen,NombreRemitente,DireccionRemitente,TelefonoRemitente,EmailRemitente,PaisDestino,NombreDestinatario,DireccionDestinatario,TelefonoDestinatario,EmailDestinatario,Total")] Guia guia)
        {
            if (ModelState.IsValid)
            {
                db.Guias.Add(guia);
                db.SaveChanges();
                //Alert("Guía creada con éxito", NotificationType.success);
                //return RedirectToAction("Index");
                return Json("Guía creada con éxito");
            }
            return View(guia);
        }

        // GET: Guia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guia guia = db.Guias.Find(id);
            if (guia == null)
            {
                return HttpNotFound();
            }
            return View(guia);
        }

        // POST: Guia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumeroGuía,FechaEnvio,PaisOrigen,NombreRemitente,DireccionRemitente,TelefonoRemitente,EmailRemitente,PaisDestino,NombreDestinatario,DireccionDestinatario,TelefonoDestinatario,EmailDestinatario,Total")] Guia guia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guia).State = EntityState.Modified;
                db.SaveChanges();

                var guiaToUpdate = db.Guias
                   .Include(i => i.Factura)
                   .Where(i => i.ID == guia.ID)
                   .Single();

                Factura factura = guiaToUpdate.Factura;
                if (factura != null)
                    FacturaRecalcular(factura);

                db.SaveChanges();
                //Alert("Guía actualizada con éxito", NotificationType.success);
                //return RedirectToAction("Index");
                return Json("Guía actualizada con éxito");
            }
            return View(guia);
        }

        // GET: Guia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guia guia = db.Guias.Find(id);
            if (guia == null)
            {
                return HttpNotFound();
            }
            return View(guia);
        }

        // POST: Guia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guia guia = db.Guias.Find(id);

            Factura factura = guia.Factura;

            db.Guias.Remove(guia);

            if (factura != null) 
                FacturaRecalcular(factura);

            db.SaveChanges();
            Alert("Guía eliminada con éxito", NotificationType.success);
            return RedirectToAction("Index");
        }

        private void FacturaRecalcular(Factura factura) {
            decimal subtotal;
            decimal impuesto;
            decimal totalactual;
            decimal total;
            decimal totaldif;

            totalactual = factura.Total;
            totaldif = 0;
            subtotal = 0;

            foreach (var guia in factura.Guias)
                subtotal += Math.Round(guia.Total, 2);

            impuesto = Math.Round((subtotal * 12) / 100, 2);
            total = Math.Round(subtotal  + impuesto, 2);

            factura.Subtotal = subtotal;
            factura.Impuesto = impuesto;
            factura.Total = total;

            totaldif = total - totalactual;
            IEnumerable<Pago> pagos = new List<Pago>();
            if (totaldif < 0)
                pagos = factura.Pagos.OrderByDescending(s => s.Valor);
            else
                pagos = factura.Pagos.OrderBy(s => s.Valor);

            foreach (var pago in pagos)
            {
                decimal valorActual = pago.Valor;
                pago.Valor = valorActual + totaldif;
                if (pago.Valor < 0)
                {
                    totaldif = pago.Valor;
                    pago.Valor = 0;
                }
                else 
                    break;
            }

            //Elimono todos los pagos en cero (0)
            pagos = pagos.Where(s => s.Valor.Equals(0));
            foreach (var pago in pagos)
                factura.Pagos.Remove(pago);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
