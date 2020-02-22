using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.Models
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public Int64 CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail{ get; set; }
        public string CustomerAddress { get; set; }
        public bool? Status { get; set; }
        public long? CreatedBy { get; set; }


    }
}
