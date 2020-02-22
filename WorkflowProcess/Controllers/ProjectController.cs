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
    public class ProjectController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        // GET: Project
        public ActionResult Index()
        {
            IEnumerable<Project> projects;
            if (Convert.ToInt32(Session["Role"]) == 1)
            {
                projects = db.Project.Include(p => p.ProjectStatus).Include(p => p.WorkFlow).ToList();
            }
            else if (Convert.ToInt32(Session["Role"]) == 3)
            {
                projects = db.Project.Include(p => p.ProjectStatus).Include(p => p.WorkFlow).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
            else
            {
                projects = db.Project.Include(p => p.ProjectStatus).Include(p => p.WorkFlow).ToList().Where(x => x.UserName == Convert.ToString(Session["Username"])).ToList();
            }
          //  var project = db.Project.Include(p => p.ProjectStatus).Include(p => p.WorkFlow);
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "ProjectStatusName");
            ViewBag.WorkflowActivityID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                var project = AutoMapper.Mapper.Map<Project>(projectModel);
                db.Project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "ProjectStatusName", projectModel.ProjectStatusId);
            ViewBag.WorkflowActivityID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", projectModel.WorkflowActivityID);
            return View(projectModel);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "ProjectStatusName", project.ProjectStatusId);
            ViewBag.WorkflowActivityID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", project.WorkflowActivityID);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,ProjectStartDate,ProjectEndDate,ProjectStatusId,WorkflowActivityID,UserName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "ProjectStatusName", project.ProjectStatusId);
            ViewBag.WorkflowActivityID = new SelectList(db.WorkFlow, "WorkflowID", "WorkflowName", project.WorkflowActivityID);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
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
