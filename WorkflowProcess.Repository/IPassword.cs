using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public interface IPassword
    {
        long? SavePassword(PasswordMaster passwordMaster);
        string GetPasswordbyUserId(string UserEmail);
        PasswordMaster GetCheckUserEmailExists(string UserEmail);
        bool CheckEmailExists(string customerEmail);
    }
}
