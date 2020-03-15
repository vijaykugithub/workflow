using WorkflowProcess.Algorithm;
using WorkflowProcess.Data;
using WorkflowProcess.Models;
using WorkflowProcess.Repository;
using WorkflowProcess.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WorkflowProcess.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _iCustomer;
        private readonly IPassword _iPassword;
        private readonly ISavedAssignedRoles _savedAssignedRoles;
        private readonly IRole _iRole;
        //private readonly IUserMaster _iUserMaster;
        public CustomerController(ICustomer customer, IPassword password, ISavedAssignedRoles savedAssignedRoles, IRole role)
        {
            _iCustomer = customer;
            _iPassword = password;
            _savedAssignedRoles = savedAssignedRoles;
            _iRole = role;
        }
        private WorkflowEntities db = new WorkflowEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var data = db.Customer.ToList();
            //return View(_iCustomer.GetAllCustomers());
            return View(data);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult LoadCustomer()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (DatabaseContext _context = new DatabaseContext())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data  
                    var customerData = (from tempcustomer in _context.Customers
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.CompanyName == searchValue
                        || m.CustomerName == searchValue);
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerViewModel customerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var isCustomer = _iCustomer.CheckCustomernameExists(customerViewModel.CustomerEmail);
                    if (isCustomer)
                    {
                        ModelState.AddModelError("", "Customer already exists");
                    }

                    AesAlgorithm aesAlgorithm = new AesAlgorithm();

                    var customer = AutoMapper.Mapper.Map<Customers>(customerViewModel);
                    customer.Status = true;
                    customer.CustomerID = 0;
                    customer.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    var customerId = _iCustomer.AddCustomer(customer);
                    if (customerId != -1)
                    {
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
                                CreateDate = DateTime.Now
                            };
                            _savedAssignedRoles.AddAssignedRoles(savedAssignedRoles);

                            TempData["MessageCreateUsers"] = "User Created Successfully";
                        }
                    }

                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    return View("Create");
                }
            }
            catch
            {
                throw;
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                using (DatabaseContext _context = new DatabaseContext())
                {
                    var Customer = (from customer in _context.Customers
                                    where customer.CustomerID == id
                                    select customer).FirstOrDefault();

                    return View(Customer);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult DeleteCustomer(int? ID)
        {
            using (DatabaseContext _context = new DatabaseContext())
            {
                var customer = _context.Customers.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }
    }
}
