using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkflowProcess.Filters;
using WorkflowProcess.Models;
using WorkflowProcess.Repository;

namespace WorkflowProcess.Controllers
{

    public class DashboardController : Controller
    {
        private readonly IMenu _iMenu;
        private readonly ISubMenu _ISubMenu;

        public DashboardController(IMenu menu , ISubMenu subMenu)
        {
            _iMenu = menu;
            _ISubMenu = subMenu;
        }

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult ShowMenus()
        {
            try
            {
                var menuList = _iMenu.GetAllActiveMenu(Convert.ToInt32(Session["Role"]));
                return PartialView("ShowMenu", menuList);
            }
            catch (Exception)
            {
                throw;
            }
        }

      

    }
}