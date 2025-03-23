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
        public Customer GetById(int id)
        {
            return repository.GetById(id);
        }
    }
}
