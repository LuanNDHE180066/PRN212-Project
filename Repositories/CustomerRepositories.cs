using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public class CustomerRepositories
    {
        public List<Customer> getAllCustomer()
        {
            return PrnFinalProjectContext.Ins.Customers.ToList();
        }
        public void AddCustomer(Customer customer)
        {
            PrnFinalProjectContext.Ins.Add(customer);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public void UpdateCustomer(Customer customer)
        {
            PrnFinalProjectContext.Ins.Update(customer);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public Customer GetCustomerByID(int id)
        {
            return PrnFinalProjectContext.Ins.Customers.Find(id);
        } 
    }
}
