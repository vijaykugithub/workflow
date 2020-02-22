using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public interface IMenu : IDisposable
    {
        List<MenuMaster> GetAllMenu();
        List<MenuMaster> GetAllActiveMenu();
        MenuMaster GetMenuById(int? menuId);
        int? AddMenu(MenuMaster menuMaster);
        int? UpdateMenu(MenuMaster menuMaster);
        void DeleteMenu(int? menuId);
        bool CheckMenuNameExists(string menuName);
        IQueryable<MenuMaster> ShowAllMenus(string sortColumn, string sortColumnDir, string search);
        List<MenuMaster> GetAllActiveMenu(long userId);
        List<MenuMaster> GetAllActiveMenuSuperAdmin();
    }
}
