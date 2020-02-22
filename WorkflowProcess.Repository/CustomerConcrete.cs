using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowProcess.Models;

namespace WorkflowProcess.Repository
{
    public class CustomerConcrete : ICustomer
    {
        private readonly DatabaseContext _context;
        private bool _disposed = false;

        public CustomerConcrete(DatabaseContext context)
        {
            _context = context;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        public long? AddCustomer(Customers customers)
        {
            try
            {
                long? result = -1;

                if (customers != null)
                {
                    _context.Customers.Add(customers);
                    _context.SaveChanges();
                    result = customers.CustomerID;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckCustomernameExists(string customername)
        {
            try
            {
                var result = (from menu in _context.Customers
                              where menu.CustomerName == customername
                              select menu).Any();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCustomer(int? customerId)
        {
            try
            {
                Customers customer = _context.Customers.Find(customerId);
                if (customer != null) _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Customers> GetAllCustomersActiveList()
        {
            throw new NotImplementedException();
        }

        public Customers GetCustomerById(int? customerId)
        {
            try
            {
                return _context.Customers.Find(customerId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Customers GetCustomersByCustomersname(string Customername)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customers> ShowAllCustomers(string sortColumn, string sortColumnDir, string search)
        {
            throw new NotImplementedException();
        }

        public long? UpdateCustomer(Customers customer)
        {
            try
            {

                long? result = -1;

                if (customer != null)
                {
                    _context.Entry(customer).State = EntityState.Modified;
                    _context.SaveChanges();
                    result = customer.CustomerID;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
