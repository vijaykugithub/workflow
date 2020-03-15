using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.ViewModels
{
   public class WorkflowActivityStreamModel
    {
        public int WorkflowActivityStreamID { get; set; }
        public Nullable<int> WorkflowID { get; set; }
        public Nullable<int> ActivityID { get; set; }
        [Required(ErrorMessage = "Predecessor value required")]
        public Nullable<int> Predecessor { get; set; }
        [Required(ErrorMessage = "Successor value required")]
        public Nullable<int> Successor { get; set; }
        [Required(ErrorMessage = "JumpTo value required")]
        public Nullable<int> JumpTo { get; set; }
        public string UserName { get; set; }
    }
}
