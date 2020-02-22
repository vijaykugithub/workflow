using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public interface ISavedMenuRoles
    {
        int SaveRole(SavedMenuRoles savedRoles);
        bool CheckRoleAlreadyExists(SavedMenuRoles savedRoles);

    }
}
