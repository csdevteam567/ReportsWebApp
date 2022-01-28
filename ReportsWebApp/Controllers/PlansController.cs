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
    public class PlansController : Controller
    {
        private ReportsWebAppDBEntities db = new ReportsWebAppDBEntities();

        // GET: Plans
        public ActionResult Index()
        {
            var plans = db.Plans.Include(p => p.City).Include(p => p.City1);
            return View(plans.ToList());
        }

        // GET: Plans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // GET: Plans/Create
        public ActionResult Create()
        {
            ViewBag.CityFromId = new SelectList(db.Cities, "Id", "CityName");
            ViewBag.CityToId = new SelectList(db.Cities, "Id", "CityName");
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Day,PlannedTransportations,CityFromId,CityToId")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                db.Plans.Add(plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityFromId = new SelectList(db.Cities, "Id", "CityName", plan.CityFromId);
            ViewBag.CityToId = new SelectList(db.Cities, "Id", "CityName", plan.CityToId);
            return View(plan);
        }

        // GET: Plans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityFromId = new SelectList(db.Cities, "Id", "CityName", plan.CityFromId);
            ViewBag.CityToId = new SelectList(db.Cities, "Id", "CityName", plan.CityToId);
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Day,PlannedTransportations,CityFromId,CityToId")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityFromId = new SelectList(db.Cities, "Id", "CityName", plan.CityFromId);
            ViewBag.CityToId = new SelectList(db.Cities, "Id", "CityName", plan.CityToId);
            return View(plan);
        }

        // GET: Plans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plan plan = db.Plans.Find(id);
            db.Plans.Remove(plan);
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
