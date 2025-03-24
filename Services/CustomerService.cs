using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    public class CustomerService
    {
        private CustomerRepository repository =new();
        private CustomerRepositories repositories = new();
        public Customer GetById(int id)
        {
            return repository.GetById(id);
        }

   public List<Customer> getAllCustomer()
        {
            return repositories.getAllCustomer();
        }
        public void AddCustomer(Customer customer)
        {
            repositories.AddCustomer(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            repositories.UpdateCustomer(customer);
        }
        public Customer GetCustomerByID(int id)
        {
            return repositories.GetCustomerByID(id);
        }
        public Customer GetCustomerLogin(string user,string pass)
        {
            return repositories.getAllCustomer().Where(s=>s.Username.Equals(user) && s.Password.Equals(pass)).First();
        } 
    }
}
