using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.ViewModels
{
    public class WorkFlowModel
    {
        public int WorkflowID { get; set; }
        [Display(Name = "Customer Name")]
        public Nullable<int> CustomerID { get; set; }
        [Required(ErrorMessage = "Workflow name required")]
        [Display(Name = "Workflow Name")]
        public string WorkflowName { get; set; }
        [Display(Name = "Workflow Description")]
        public string WorkflowDescription { get; set; }
        public string UserName { get; set; }
    }
}
