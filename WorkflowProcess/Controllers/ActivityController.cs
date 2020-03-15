using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowProcess.Data;
using WorkflowProcess.Filters;
using WorkflowProcess.Repository;
using WorkflowProcess.ViewModels;

namespace WorkflowProcess.Controllers
{
    [AuthorizeSuperAdminandAdmin]
    public class ActivityController : Controller
    {
        //private readonly ICustomer _iCustomer;
        public ActivityController()
        {
            //_iCustomer = customer;
        }
        private WorkflowEntities db = new WorkflowEntities();

        // GET: Activity
        public ActionResult Index()
        {
            IEnumerable<Activity> activity;
            if (Convert.ToInt32(Session["Role"]) == 1)
            {
                activity = db.Activity.Include(p => p.Customer).ToList();
            }
            else
            {
                activity = db.Activity.Include(p => p.Customer).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
          //  var activity = db.Activity.Include(a => a.Customer);
            return View(activity);
        }

        // GET: Activity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activity/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            return View();
        }

        // POST: Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityModel activityModel)
        {
            if (ModelState.IsValid)
            {
                var activity = AutoMapper.Mapper.Map<Activity>(activityModel);
                activity.UserName= Convert.ToString(Session["Username"]);
                db.Activity.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", activityModel.CustomerId);
            return View(activityModel);
        }

        // GET: Activity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", activity.CustomerId);
            return View(activity);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,CustomerId,ActivityName,ActivityDescription")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activity.UserName = Convert.ToString(Session["Username"]);
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", activity.CustomerId);
            return View(activity);
        }

        // GET: Activity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
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
