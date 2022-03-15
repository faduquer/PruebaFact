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
using PruebaFact.ViewModels;
using PagedList;
using static PruebaFact.Models.EnumAlert;

namespace PruebaFact.Controllers
{
    public class FacturaController : BaseController
    {
        private FacturaContext db = new FacturaContext();

        // GET: Factura
        public ActionResult Index(
            string sortOrder,
            string actgualNumeroFactura,
            string busquedaNumeroFactura,
            string actgualEstablecimiento,
            string busquedaEstablecimiento,
            int? page)
        {
            if (sortOrder == null)
                sortOrder = "date_desc";

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumeroFacturaSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FechaEmisionSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (busquedaNumeroFactura != null || busquedaEstablecimiento != null)
            {
                page = 1;
            }
            else
            {
                busquedaNumeroFactura = actgualNumeroFactura;
                busquedaEstablecimiento = actgualEstablecimiento;
            }

            ViewBag.actgualNumeroFactura = busquedaNumeroFactura;
            ViewBag.actgualEstablecimiento = busquedaEstablecimiento;

            var facturas = from s in db.Facturas
                        select s;

            if (!String.IsNullOrEmpty(busquedaNumeroFactura))
                facturas = facturas.Where(s => s.ID.ToString().Contains(busquedaNumeroFactura));

            if (!String.IsNullOrEmpty(busquedaEstablecimiento))
                facturas = facturas.Where(s => s.Establecimiento.Contains(busquedaEstablecimiento));

            switch (sortOrder)
            {
                case "name_desc":
                    facturas = facturas.OrderByDescending(s => s.ID);
                    break;
                case "Date":
                    facturas = facturas.OrderBy(s => s.FechaEmision);
                    break;
                case "date_desc":
                    facturas = facturas.OrderByDescending(s => s.FechaEmision);
                    break;
                default:
                    facturas = facturas.OrderBy(s => s.ID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(facturas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Factura/Create
        public ActionResult Create()
        {
            var factura = new Factura();
            factura.Guias = new List<Guia>();
            GuiasAsignadasData(factura.ID);
            PagosAsignadosData(factura.ID);
            return View();
        }

        // POST: Factura/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Establecimiento,PuntoEmision,Secuencial,FechaEmision,Subtotal,Impuesto,Total")] Factura factura, string[] guiasSeleccionadas, string[] pagosSeleccionados)
        {
            if (guiasSeleccionadas != null) 
            {
                factura.Guias = new List<Guia>();
                foreach (var guiaId in guiasSeleccionadas)
                {
                    var guia = db.Guias.Find(int.Parse(guiaId));
                    factura.Guias.Add(guia);
                }
            }

            if (pagosSeleccionados != null)
            {
                factura.Pagos = new List<Pago>();
                foreach (var pagoId in pagosSeleccionados)
                {
                    var pago = db.Pagos.Find(int.Parse(pagoId));
                    factura.Pagos.Add(pago);
                }
            }

            if (ModelState.IsValid)
            {
                db.Facturas.Add(factura);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json("Factura creada con éxito");
            }
            return View(factura);
        }

        // GET: Factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            GuiasAsignadasData(factura.ID);
            PagosAsignadosData(factura.ID);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Factura/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Establecimiento,PuntoEmision,Secuencial,FechaEmision,Subtotal,Impuesto,Total")] Factura factura, string[] guiasSeleccionadas, string[] pagosSeleccionados)
        {
            if (ModelState.IsValid)
            {
                var facturaToUpdate = db.Facturas
                   .Include(i => i.Guias)
                   .Include(i => i.Pagos)
                   .Where(i => i.ID == factura.ID)
                   .Single();

                db.Entry(facturaToUpdate).State = EntityState.Modified;
                ActualizarFactura(guiasSeleccionadas, pagosSeleccionados, facturaToUpdate);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json("Factura actualizada con éxito");
            }
            //else {
            //   Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //   return Json("Model is not valid");
            //}
            return View(factura);
        }

        private void ActualizarFactura(string[] guiasSeleccionadas, string[] pagosSeleccionados, Factura factura)
        {
            if (guiasSeleccionadas == null)
            {
                factura.Guias = new List<Guia>();
            }
            else {
                var guiasSeleccionadasHS = new HashSet<string>(guiasSeleccionadas);
                var guiasDeFactura = new HashSet<int>(factura.Guias.Select(c => c.ID));
                foreach (var guia in db.Guias)
                {
                    if (guiasSeleccionadasHS.Contains(guia.ID.ToString()))
                    {
                        if (!guiasDeFactura.Contains(guia.ID))
                            factura.Guias.Add(guia);
                    }
                    else
                    {
                        if (guiasDeFactura.Contains(guia.ID))
                            factura.Guias.Remove(guia);
                    }
                }
            }

            if (pagosSeleccionados == null)
            {
                factura.Pagos = new List<Pago>();
            }
            else
            {
                var pagosSeleccionadosHS = new HashSet<string>(pagosSeleccionados);
                var pagosDeFactura = new HashSet<int>(factura.Pagos.Select(c => c.ID));
                foreach (var pago in db.Pagos)
                {
                    if (pagosSeleccionadosHS.Contains(pago.ID.ToString()))
                    {
                        if (!pagosDeFactura.Contains(pago.ID))
                            factura.Pagos.Add(pago);
                    }
                    else
                    {
                        if (pagosDeFactura.Contains(pago.ID))
                            factura.Pagos.Remove(pago);
                    }
                }
            }
        }

        // GET: Factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Facturas.Find(id); 

            foreach (var itemPago in factura.Pagos)
                itemPago.Factura = null;

            foreach (var itemGuia in factura.Guias)
                itemGuia.Factura = null;

            db.Facturas.Remove(factura);

            db.SaveChanges();
            Alert("Factura eliminada con éxito", NotificationType.success);
            return RedirectToAction("Index");
        }

        private void GuiasAsignadasData(int facturaID)
        {
            var listaGuias = db.Guias.Where(s => s.Factura == null || s.Factura.ID == facturaID);
            var viewModel = new List<GuiaAsignadaData>();
            foreach (var guia in listaGuias)
            {
                viewModel.Add(new GuiaAsignadaData
                {
                    ID = guia.ID,
                    NumeroGuía = guia.NumeroGuía,
                    FechaEnvio = guia.FechaEnvio,
                    Total = guia.Total,
                    Asignada = (guia.Factura == null ? false : guia.Factura.ID == facturaID)
                });
            }
            ViewBag.Guias = viewModel;
        }

        private void PagosAsignadosData(int facturaID)
        {
            var listaPagos = db.Pagos.Where(s => s.Factura == null || s.Factura.ID == facturaID);
            var viewModel = new List<PagoAsignadoData>();
            foreach (var pago in listaPagos)
            {
                viewModel.Add(new PagoAsignadoData
                {
                    ID = pago.ID,
                    TipoPago = pago.TipoPago,
                    Valor = pago.Valor,
                    Asignado = (pago.Factura == null ? false : pago.Factura.ID == facturaID)
                });
            }
            ViewBag.Pagos = viewModel;
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
