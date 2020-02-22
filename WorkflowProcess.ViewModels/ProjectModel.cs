using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.ViewModels
{
  public  class ProjectModel
    {
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Project name required")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Project start date required")]
        [DataType(DataType.Date)]
        [Display(Name = "Project Start Date")]
        public Nullable<System.DateTime> ProjectStartDate { get; set; }

        [Required(ErrorMessage = "Project end date required")]
        [DataType(DataType.Date)]
        [Display(Name = "Project End Date")]
        public Nullable<System.DateTime> ProjectEndDate { get; set; }
        [Display(Name ="ProjectStatus Name")]
        //[Required(ErrorMessage = "ProjectStatus name required")]
        public Nullable<int> ProjectStatusId { get; set; }
        [Display(Name = "WorkflowActivity Name")]
        //[Required(ErrorMessage = "WorkflowActivity name required")]
        public Nullable<int> WorkflowActivityID { get; set; }
        public string UserName { get; set; }

        //public virtual ProjectStatus ProjectStatus { get; set; }
        //public virtual WorkFlow WorkFlow { get; set; }

        //public virtual ICollection<ProjectActivity> ProjectActivity { get; set; }
        //public virtual ICollection<ProjectActivityConfiguration> ProjectActivityConfiguration { get; set; }
    }
}
