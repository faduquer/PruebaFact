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
    public class PagoController : BaseController
    {
        private FacturaContext db = new FacturaContext();

        // GET: Pago
        public ActionResult Index(
            string sortOrder,
            string actgualIdPago,
            string busquedaIdPago,
            string actgualIdFactura,
            string busquedaIdFactura,
            int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdPagoSortParm = String.IsNullOrEmpty(sortOrder) ? "idpago" : "";
            ViewBag.IdFacturaSortParm = sortOrder == "idfactura" ? "idfactura_desc" : "idfactura";

            if (busquedaIdPago != null || busquedaIdFactura != null)
            {
                page = 1;
            }
            else
            {
                busquedaIdPago = actgualIdPago;
                busquedaIdFactura = actgualIdFactura;
            }

            ViewBag.actgualIdPago = busquedaIdPago;
            ViewBag.actgualIdFactura = busquedaIdFactura;

            var pagos = from s in db.Pagos
                        select s;

            if (!String.IsNullOrEmpty(busquedaIdPago))
                pagos = pagos.Where(s => s.ID.ToString().Contains(busquedaIdPago));

            if (!String.IsNullOrEmpty(busquedaIdFactura))
                pagos = pagos.Where(s => s.Factura.ID.ToString().Contains(busquedaIdFactura));

            switch (sortOrder)
            {
                case "idfactura":
                    pagos = pagos.OrderBy(s => s.Factura.ID);
                    break;
                case "idfactura_desc":
                    pagos = pagos.OrderByDescending(s => s.Factura.ID);
                    break;
                case "idpago":
                    pagos = pagos.OrderBy(s => s.ID);
                    break;
                default:
                    pagos = pagos.OrderByDescending(s => s.ID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pagos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TipoPago,Valor")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Pagos.Add(pago);
                db.SaveChanges();
                //Alert("Pago creado con éxito", NotificationType.success);
                //return RedirectToAction("Index");
                return Json("Pago creado con éxito");
            }
            return View(pago);
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TipoPago,Valor")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();
                //Alert("Pago actualizado con éxito", NotificationType.success);
                //return RedirectToAction("Index");
                return Json("Pago actualizado con éxito");
            }
            return View(pago);
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago pago = db.Pagos.Find(id);
            db.Pagos.Remove(pago);
            db.SaveChanges();
            Alert("PAgo eliminado con éxito", NotificationType.success);
            return RedirectToAction("Index");
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
