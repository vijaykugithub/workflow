using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowProcess.Data;

namespace WorkflowProcess.Controllers
{
    public class ActivityStatusController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: ActivityStatus
        public ActionResult Index()
        {
            return View(db.ActivityStatus.ToList());
        }

        // GET: ActivityStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityStatus activityStatus = db.ActivityStatus.Find(id);
            if (activityStatus == null)
            {
                return HttpNotFound();
            }
            return View(activityStatus);
        }

        // GET: ActivityStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityStatusId,ActivityStatusName,UserName")] ActivityStatus activityStatus)
        {
            if (ModelState.IsValid)
            {
                db.ActivityStatus.Add(activityStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityStatus);
        }

        // GET: ActivityStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityStatus activityStatus = db.ActivityStatus.Find(id);
            if (activityStatus == null)
            {
                return HttpNotFound();
            }
            return View(activityStatus);
        }

        // POST: ActivityStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityStatusId,ActivityStatusName,UserName")] ActivityStatus activityStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityStatus);
        }

        // GET: ActivityStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityStatus activityStatus = db.ActivityStatus.Find(id);
            if (activityStatus == null)
            {
                return HttpNotFound();
            }
            return View(activityStatus);
        }

        // POST: ActivityStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityStatus activityStatus = db.ActivityStatus.Find(id);
            db.ActivityStatus.Remove(activityStatus);
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
