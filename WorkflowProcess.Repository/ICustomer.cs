using WorkflowProcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowProcess.Repository
{
    public interface ICustomer
    {
        List<Customers> GetAllCustomers();
        Customers GetCustomerById(int? customerId);
        long? AddCustomer(Customers customers);
        long? UpdateCustomer(Customers customer);
        void DeleteCustomer(int? customerId);
        bool CheckCustomernameExists(string customername);
        Customers GetCustomersByCustomersname(string Customername);
        IQueryable<Customers> ShowAllCustomers(string sortColumn, string sortColumnDir, string search);
        List<Customers> GetAllCustomersActiveList();
    }
}
