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
    public class WorkflowActivityStreamController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: WorkflowActivityStream
        public ActionResult Index()
        {
            var workflowActivityStream = db.WorkflowActivityStream.Include(w => w.Activity).Include(w => w.WorkFlow);
            return View(workflowActivityStream.ToList());
        }

        // GET: WorkflowActivityStream/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkflowActivityStream workflowActivityStream = db.WorkflowActivityStream.Find(id);
            if (workflowActivityStream == null)
            {
                return HttpNotFound();
            }
            return View(workflowActivityStream);
        }

        // GET: WorkflowActivityStream/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName");
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName");
            return View();
        }

        // POST: WorkflowActivityStream/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkflowActivityStreamID,WorkflowID,ActivityID,Predecessor,Successor,JumpTo,UserName")] WorkflowActivityStream workflowActivityStream)
        {
            if (ModelState.IsValid)
            {
                db.WorkflowActivityStream.Add(workflowActivityStream);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName", workflowActivityStream.ActivityID);
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", workflowActivityStream.WorkflowID);
            return View(workflowActivityStream);
        }

        // GET: WorkflowActivityStream/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkflowActivityStream workflowActivityStream = db.WorkflowActivityStream.Find(id);
            if (workflowActivityStream == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName", workflowActivityStream.ActivityID);
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", workflowActivityStream.WorkflowID);
            return View(workflowActivityStream);
        }

        // POST: WorkflowActivityStream/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkflowActivityStreamID,WorkflowID,ActivityID,Predecessor,Successor,JumpTo,UserName")] WorkflowActivityStream workflowActivityStream)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflowActivityStream).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName", workflowActivityStream.ActivityID);
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", workflowActivityStream.WorkflowID);
            return View(workflowActivityStream);
        }

        // GET: WorkflowActivityStream/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkflowActivityStream workflowActivityStream = db.WorkflowActivityStream.Find(id);
            if (workflowActivityStream == null)
            {
                return HttpNotFound();
            }
            return View(workflowActivityStream);
        }

        // POST: WorkflowActivityStream/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkflowActivityStream workflowActivityStream = db.WorkflowActivityStream.Find(id);
            db.WorkflowActivityStream.Remove(workflowActivityStream);
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
