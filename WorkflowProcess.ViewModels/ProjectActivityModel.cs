using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.ViewModels
{
   public class ProjectActivityModel
    {

        public int ProjectActivityId { get; set; }
        [Required(ErrorMessage = "Project name required")]
        public Nullable<int> ProjectId { get; set; }
        [Required(ErrorMessage = "Activity name required")]
        public Nullable<int> ActivityId { get; set; }
        [Required(ErrorMessage = "ActivityStatus name required")]
        public Nullable<int> ActivityStatusId { get; set; }
       // public string UserName { get; set; }

        //public virtual Activity Activity { get; set; }
        //public virtual ActivityStatus ActivityStatus { get; set; }
        //public virtual Project Project { get; set; }
    }
}
