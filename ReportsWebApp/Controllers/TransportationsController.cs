using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReportsWebApp.Models;

namespace ReportsWebApp.Controllers
{
    public class TransportationsController : Controller
    {
        private ReportsWebAppDBEntities db = new ReportsWebAppDBEntities();

        // GET: Transportations
        [DatabaseAuthorizeFilter]
        public ActionResult Index()
        {
            var transportations = db.Transportations.Include(t => t.Plan);
            return View(transportations.ToList());
        }

        // GET: Transportations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // GET: Transportations/Create
        public ActionResult Create()
        {
            ViewBag.PlanId = new SelectList(db.Plans, "Id", "Id");
            return View();
        }

        // POST: Transportations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateTimeDeparture,PlanId")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                db.Transportations.Add(transportation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanId = new SelectList(db.Plans, "Id", "Id", transportation.PlanId);
            return View(transportation);
        }

        // GET: Transportations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanId = new SelectList(db.Plans, "Id", "Id", transportation.PlanId);
            return View(transportation);
        }

        // POST: Transportations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTimeDeparture,PlanId")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlanId = new SelectList(db.Plans, "Id", "Id", transportation.PlanId);
            return View(transportation);
        }

        // GET: Transportations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // POST: Transportations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transportation transportation = db.Transportations.Find(id);
            db.Transportations.Remove(transportation);
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
