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
using WorkflowProcess.ViewModels;

namespace WorkflowProcess.Controllers
{
    [AuthorizeSuperAdminandAdmin]
    public class ProjectActivityController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: ProjectActivity
        public ActionResult Index()
        {
            IEnumerable<ProjectActivity> projectActivity;
            if (Convert.ToInt32(Session["Role"]) == 1)
            {
                projectActivity = db.ProjectActivity.Include(p => p.Activity).Include(p => p.ActivityStatus).Include(p => p.Project).ToList();
            }
            else if (Convert.ToInt32(Session["Role"]) == 3)
            {
                projectActivity = db.ProjectActivity.Include(p => p.Activity).Include(p => p.ActivityStatus).Include(p => p.Project).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
            else
            {
                projectActivity = db.ProjectActivity.Include(p => p.Activity).Include(p => p.ActivityStatus).Include(p => p.Project).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
          //  var projectActivity = db.ProjectActivity.Include(p => p.Activity).Include(p => p.ActivityStatus).Include(p => p.Project);
            return View(projectActivity);
        }

        // GET: ProjectActivity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectActivity projectActivity = db.ProjectActivity.Find(id);
            if (projectActivity == null)
            {
                return HttpNotFound();
            }
            return View(projectActivity);
        }

        // GET: ProjectActivity/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activity, "ActivityId", "ActivityName");
            ViewBag.ActivityStatusId = new SelectList(db.ActivityStatus, "ActivityStatusId", "ActivityStatusName");
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: ProjectActivity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectActivityModel projectActivity)
        {
            if (ModelState.IsValid)
            {
                var project = AutoMapper.Mapper.Map<ProjectActivity>(projectActivity);
                db.ProjectActivity.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activity, "ActivityId", "ActivityName", projectActivity.ActivityId);
            ViewBag.ActivityStatusId = new SelectList(db.ActivityStatus, "ActivityStatusId", "ActivityStatusName", projectActivity.ActivityStatusId);
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", projectActivity.ProjectId);
            return View(projectActivity);
        }

        // GET: ProjectActivity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectActivity projectActivity = db.ProjectActivity.Find(id);
            if (projectActivity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "ActivityId", "ActivityName", projectActivity.ActivityId);
            ViewBag.ActivityStatusId = new SelectList(db.ActivityStatus, "ActivityStatusId", "ActivityStatusName", projectActivity.ActivityStatusId);
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", projectActivity.ProjectId);
            return View(projectActivity);
        }

        // POST: ProjectActivity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectActivityModel projectActivity)
        {
            if (ModelState.IsValid)
            {
                var project = AutoMapper.Mapper.Map<ProjectActivity>(projectActivity);
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "ActivityId", "ActivityName", projectActivity.ActivityId);
            ViewBag.ActivityStatusId = new SelectList(db.ActivityStatus, "ActivityStatusId", "ActivityStatusName", projectActivity.ActivityStatusId);
            ViewBag.ProjectId = new SelectList(db.Project, "ProjectId", "ProjectName", projectActivity.ProjectId);
            return View(projectActivity);
        }

        // GET: ProjectActivity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectActivity projectActivity = db.ProjectActivity.Find(id);
            if (projectActivity == null)
            {
                return HttpNotFound();
            }
            return View(projectActivity);
        }

        // POST: ProjectActivity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectActivity projectActivity = db.ProjectActivity.Find(id);
            db.ProjectActivity.Remove(projectActivity);
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
