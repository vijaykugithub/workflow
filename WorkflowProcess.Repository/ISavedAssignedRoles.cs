using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public interface ISavedAssignedRoles
    {
        long? AddAssignedRoles(SavedAssignedRoles savedAssignedRoles);
        bool CheckAssignedRoles(long? userId);
        SavedAssignedRoles GetAssignedRolesbyUserId(long? userId);
    }
}
