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
    public class WorkFlowController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: WorkFlow
        public ActionResult Index()
        {
            IEnumerable<WorkFlow> workFlow;
            if (Convert.ToInt32(Session["Role"]) == 1)
            {
                workFlow = db.WorkFlow.Include(w => w.Customer).ToList();
            }
            else
            {
                workFlow = db.WorkFlow.Include(w => w.Customer).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
            return View(workFlow);
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
        public ActionResult Create(WorkFlowModel workFlowModel)
        {
            if (ModelState.IsValid)
            {
                var workFlow = AutoMapper.Mapper.Map<WorkFlow>(workFlowModel);
                workFlow.UserName = Convert.ToString(Session["Username"]);
                db.WorkFlow.Add(workFlow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName", workFlowModel.CustomerID);
            return View(workFlowModel);
        }

        // GET: WorkFlow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkFlow workFlow = db.WorkFlow.Find(id);
            var workFlows = AutoMapper.Mapper.Map<WorkFlowModel>(workFlow);
            //workFlowModel = workFlow;
            if (workFlows == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName", workFlows.CustomerID);
            return View(workFlows);
        }

        // POST: WorkFlow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkFlowModel workFlowModel)
        {
            if (ModelState.IsValid)
            {
                var workFlow = AutoMapper.Mapper.Map<WorkFlow>(workFlowModel);
                db.Entry(workFlow).State = EntityState.Modified;
                workFlow.UserName = Convert.ToString(Session["Username"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerId", "CustomerName", workFlowModel.CustomerID);
            return View(workFlowModel);
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
