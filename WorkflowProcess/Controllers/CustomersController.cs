using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowProcess.Algorithm;
using WorkflowProcess.Data;
using WorkflowProcess.Models;
using WorkflowProcess.Repository;
using WorkflowProcess.ViewModels;

namespace WorkflowProcess.Controllers
{
    public class CustomersController : Controller
    {
        private WorkflowEntities db = new WorkflowEntities();

        private readonly IPassword _iPassword;
        private readonly ISavedAssignedRoles _savedAssignedRoles;
        private readonly IRole _iRole;
        //private readonly IUserMaster _iUserMaster;
        public CustomersController(IPassword password, ISavedAssignedRoles savedAssignedRoles, IRole role)
        {
            _iPassword = password;
            _savedAssignedRoles = savedAssignedRoles;
            _iRole = role;
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {
                Customer customerObject =   db.Customer.Where(x => x.CustomerEmail == customerViewModel.CustomerEmail).FirstOrDefault();
                if (customerObject == null)
                {
                    ModelState.AddModelError("", "Customer already exists");
                }
                AesAlgorithm aesAlgorithm = new AesAlgorithm();

                var customer = AutoMapper.Mapper.Map<Customer>(customerViewModel);
                customer.Status = true;
                customer.CustomerId = 0;
                customer.CreatedBy = Convert.ToInt32(Session["UserID"]);
                db.Customer.Add(customer);
                db.SaveChanges();
                int customerId = customer.CustomerId; 
                var passwordMaster = new PasswordMaster
                {
                    CreateDate = DateTime.Now,
                    UserId = customerId,
                    PasswordId = 0,
                    Password = aesAlgorithm.EncryptString(customerViewModel.Password),
                    UserEmail = customerViewModel.CustomerEmail
                };

                var passwordId = _iPassword.SavePassword(passwordMaster);
                if (passwordId != -1)
                {
                    var savedAssignedRoles = new SavedAssignedRoles()
                    {
                        RoleId = 3,
                        UserId = customerId,
                        AssignedRoleId = 0,
                        Status = true,
                        CreateDate = DateTime.Now,
                    };
                    _savedAssignedRoles.AddAssignedRoles(savedAssignedRoles);

                    TempData["MessageCreateUsers"] = "User Created Successfully";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create");
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerName,CustomerAddress,UserName,CompanyName,Status,CreatedBy,CustomerEmail")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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
