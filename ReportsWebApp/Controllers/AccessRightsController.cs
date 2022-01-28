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
    public class AccessRightsController : Controller
    {
        private ReportsWebAppDBEntities db = new ReportsWebAppDBEntities();

        // GET: AccessRights
        public ActionResult Index()
        {
            var accessRights = db.AccessRights.Include(a => a.AspNetUser);
            return View(accessRights.ToList());
        }

        // GET: AccessRights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRight accessRight = db.AccessRights.Find(id);
            if (accessRight == null)
            {
                return HttpNotFound();
            }
            return View(accessRight);
        }

        // GET: AccessRights/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: AccessRights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupReport,PlanReport,Database,ManageUsers,AspNetUserId")] AccessRight accessRight)
        {
            if (ModelState.IsValid)
            {
                db.AccessRights.Add(accessRight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", accessRight.AspNetUserId);
            return View(accessRight);
        }

        // GET: AccessRights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRight accessRight = db.AccessRights.Find(id);
            if (accessRight == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", accessRight.AspNetUserId);
            return View(accessRight);
        }

        // POST: AccessRights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupReport,PlanReport,Database,ManageUsers,AspNetUserId")] AccessRight accessRight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessRight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", accessRight.AspNetUserId);
            return View(accessRight);
        }

        // GET: AccessRights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRight accessRight = db.AccessRights.Find(id);
            if (accessRight == null)
            {
                return HttpNotFound();
            }
            return View(accessRight);
        }

        // POST: AccessRights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessRight accessRight = db.AccessRights.Find(id);
            db.AccessRights.Remove(accessRight);
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
