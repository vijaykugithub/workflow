using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseConnection")
        {
        }

        public DbSet<MenuMaster> MenuMaster { get; set; }
        public DbSet<SubMenuMaster> SubMenuMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<Usermaster> Usermasters { get; set; }
        public DbSet<SavedMenuRoles> SavedMenuRoles { get; set; }
        public DbSet<SavedSubMenuRoles> SavedSubMenuRoles { get; set; }
        public DbSet<PasswordMaster> PasswordMaster { get; set; }
        public DbSet<SavedAssignedRoles> SavedAssignedRoles { get; set; }
        public DbSet<Customers> Customers { get; set; }


    }
}
