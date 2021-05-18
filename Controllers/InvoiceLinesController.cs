using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamProject.Models;

namespace ExamProject.Controllers
{
    public class InvoiceLinesController : Controller
    {
        private ChinookEntities db = new ChinookEntities();

        // GET: InvoiceLines
        public ActionResult Index()
        {
            var invoiceLine = db.InvoiceLine.Include(i => i.Invoice).Include(i => i.Track);
            return View(invoiceLine.ToList());
        }

        // GET: InvoiceLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLine invoiceLine = db.InvoiceLine.Find(id);
            if (invoiceLine == null)
            {
                return HttpNotFound();
            }
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceId = new SelectList(db.Invoice, "InvoiceId", "BillingAddress");
            ViewBag.TrackId = new SelectList(db.Track, "TrackId", "Name");
            return View();
        }

        // POST: InvoiceLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceLineId,InvoiceId,TrackId,UnitPrice,Quantity")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceLine.Add(invoiceLine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceId = new SelectList(db.Invoice, "InvoiceId", "BillingAddress", invoiceLine.InvoiceId);
            ViewBag.TrackId = new SelectList(db.Track, "TrackId", "Name", invoiceLine.TrackId);
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLine invoiceLine = db.InvoiceLine.Find(id);
            if (invoiceLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoice, "InvoiceId", "BillingAddress", invoiceLine.InvoiceId);
            ViewBag.TrackId = new SelectList(db.Track, "TrackId", "Name", invoiceLine.TrackId);
            return View(invoiceLine);
        }

        // POST: InvoiceLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceLineId,InvoiceId,TrackId,UnitPrice,Quantity")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoice, "InvoiceId", "BillingAddress", invoiceLine.InvoiceId);
            ViewBag.TrackId = new SelectList(db.Track, "TrackId", "Name", invoiceLine.TrackId);
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLine invoiceLine = db.InvoiceLine.Find(id);
            if (invoiceLine == null)
            {
                return HttpNotFound();
            }
            return View(invoiceLine);
        }

        // POST: InvoiceLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceLine invoiceLine = db.InvoiceLine.Find(id);
            db.InvoiceLine.Remove(invoiceLine);
            db.SaveChanges();
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
