using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public interface ISavedSubMenuRoles
    {
        int SaveRole(SavedSubMenuRoles savedRoles);
        bool CheckRoleAlreadyExists(SavedSubMenuRoles savedSubMenuRoles);

    }
}
