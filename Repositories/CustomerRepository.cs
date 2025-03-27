using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories
{
    public class CustomerRepository
    {
        private PrnFinalProjectContext _projectContext;

        public Customer GetById(int id)
        {
            _projectContext = new PrnFinalProjectContext();
            return _projectContext.Customers.FirstOrDefault(x => x.Cid == id);
        }

        public void Update(Customer customer)
        {
            _projectContext = new();
            Customer existingCustomer = _projectContext.Customers.Find(customer.Cid);



            if (existingCustomer != null)
            {

                _projectContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
            }
            else
            {
                _projectContext.Customers.Update(customer);
            }
            _projectContext.SaveChanges();
        }

        public Customer GetByUsername(string username)
        {
            _projectContext = new();
            return _projectContext.Customers.FirstOrDefault(x => x.Username.Equals(username));
        }
    }
}

