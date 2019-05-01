using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gorm_Quiz2.Models;

namespace gorm_Quiz2.Controllers
{
    public class WardDetailsController : Controller
    {
        private Entities4 db = new Entities4();

        // GET: WardDetails
        public ActionResult Index()
        {
            var wardDetails = db.WardDetails.Include(w => w.Patient);
            return View(wardDetails.ToList());
        }

        // GET: WardDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardDetail wardDetail = db.WardDetails.Find(id);
            if (wardDetail == null)
            {
                return HttpNotFound();
            }
            return View(wardDetail);
        }

        // GET: WardDetails/Create
        public ActionResult Create()
        {
            ViewBag.PId = new SelectList(db.Patients, "PId", "PFirstName");
            return View();
        }

        // POST: WardDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WardId,WardName,PId")] WardDetail wardDetail)
        {
            if (ModelState.IsValid)
            {
                db.WardDetails.Add(wardDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PId = new SelectList(db.Patients, "PId", "PFirstName", wardDetail.PId);
            return View(wardDetail);
        }

        // GET: WardDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardDetail wardDetail = db.WardDetails.Find(id);
            if (wardDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PId = new SelectList(db.Patients, "PId", "PFirstName", wardDetail.PId);
            return View(wardDetail);
        }

        // POST: WardDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WardId,WardName,PId")] WardDetail wardDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wardDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PId = new SelectList(db.Patients, "PId", "PFirstName", wardDetail.PId);
            return View(wardDetail);
        }

        // GET: WardDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardDetail wardDetail = db.WardDetails.Find(id);
            if (wardDetail == null)
            {
                return HttpNotFound();
            }
            return View(wardDetail);
        }

        // POST: WardDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WardDetail wardDetail = db.WardDetails.Find(id);
            db.WardDetails.Remove(wardDetail);
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
