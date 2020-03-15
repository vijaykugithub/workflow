using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.ViewModels
{
   public class ActivityModel
    {
        public int ActivityId { get; set; }
        [Display(Name = "Customer Name")]
        public Nullable<int> CustomerId { get; set; }
        [Required(ErrorMessage = "Activity name required")]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }
        [Display(Name = "Activity Description")]
        public string ActivityDescription { get; set; }
    }
}
