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
    public class WorkFlowController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: WorkFlow
        public ActionResult Index()
        {
            var workFlow = db.WorkFlow.Include(w => w.Customer);
            return View(workFlow.ToList());
        }

        // GET: WorkFlow/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = db.WorkFlow.Find(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }

        // GET: WorkFlow/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName");
            return View();
        }

        // POST: WorkFlow/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkflowID,CustomerID,WorkflowName,WorkflowDescription,UserName")] WorkFlow workFlow)
        {
            if (ModelState.IsValid)
            {
                db.WorkFlow.Add(workFlow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName", workFlow.CustomerID);
            return View(workFlow);
        }

        // GET: WorkFlow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = db.WorkFlow.Find(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName", workFlow.CustomerID);
            return View(workFlow);
        }

        // POST: WorkFlow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkflowID,CustomerID,WorkflowName,WorkflowDescription,UserName")] WorkFlow workFlow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workFlow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName", workFlow.CustomerID);
            return View(workFlow);
        }

        // GET: WorkFlow/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = db.WorkFlow.Find(id);
            if (workFlow == null)
            {
                return HttpNotFound();
            }
            return View(workFlow);
        }

        // POST: WorkFlow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkFlow workFlow = db.WorkFlow.Find(id);
            db.WorkFlow.Remove(workFlow);
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
