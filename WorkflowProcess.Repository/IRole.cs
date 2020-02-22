using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;
using WorkflowProcess.ViewModels;

namespace WorkflowProcess.Repository
{
    public interface IRole
    {
        IEnumerable<RoleMaster> GetAllRoleMaster();
        RoleMaster GetRoleMasterById(int? roleId);
        int? AddRoleMaster(RoleMaster roleMaster);
        int? UpdateRoleMaster(RoleMaster roleMaster);
        void DeleteRoleMaster(int? roleId);
        bool CheckRoleMasterNameExists(string roleName);
        IQueryable<RoleMaster> ShowAllRoleMaster(string sortColumn, string sortColumnDir, string search);
        List<RoleMaster> GetAllActiveRoles();
        int? UpdateRoleStatus(ViewMenuRoleStatusUpdateModel vmrolemodel);
        int? UpdateSubMenuRoleStatus(ViewSubMenuRoleStatusUpdateModel vmrolemodel);
    }
}
