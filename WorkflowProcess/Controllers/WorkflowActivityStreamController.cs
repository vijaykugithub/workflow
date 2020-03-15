using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowProcess.Data;
using WorkflowProcess.ViewModels;

namespace WorkflowProcess.Controllers
{
    public class WorkflowActivityStreamController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: WorkflowActivityStream
        public ActionResult Index()
        {
            IEnumerable<WorkflowActivityStream> workflowActivityStream;
            if (Convert.ToInt32(Session["Role"]) == 1)
            {
                workflowActivityStream = db.WorkflowActivityStream.Include(w => w.Activity).Include(w => w.WorkFlow).ToList();
            }
            else
            {
                workflowActivityStream = db.WorkflowActivityStream.Include(w => w.Activity).Include(w => w.WorkFlow).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
            return View(workflowActivityStream);
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
        public ActionResult Create(WorkflowActivityStreamModel workflowActivityStreamModel)
        {
            if (ModelState.IsValid)
            {
                var workflowActivityStream = AutoMapper.Mapper.Map<WorkflowActivityStream>(workflowActivityStreamModel);
                workflowActivityStream.UserName = Convert.ToString(Session["Username"]);
                db.WorkflowActivityStream.Add(workflowActivityStream);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName", workflowActivityStreamModel.ActivityID);
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", workflowActivityStreamModel.WorkflowID);
            return View(workflowActivityStreamModel);
        }

        // GET: WorkflowActivityStream/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkflowActivityStream workflowActivityStream = db.WorkflowActivityStream.Find(id);
            var workflowActivityStreams = AutoMapper.Mapper.Map<WorkflowActivityStreamModel>(workflowActivityStream);
            if (workflowActivityStreams == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName", workflowActivityStreams.ActivityID);
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", workflowActivityStreams.WorkflowID);
            return View(workflowActivityStreams);
        }

        // POST: WorkflowActivityStream/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkflowActivityStreamModel workflowActivityStreamModel)
        {
            if (ModelState.IsValid)
            {
                var workflowActivityStream = AutoMapper.Mapper.Map<WorkflowActivityStream>(workflowActivityStreamModel);
                workflowActivityStream.UserName = Convert.ToString(Session["Username"]);
                db.Entry(workflowActivityStream).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activity, "ActivityId", "ActivityName", workflowActivityStreamModel.ActivityID);
            ViewBag.WorkflowID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", workflowActivityStreamModel.WorkflowID);
            return View(workflowActivityStreamModel);
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
