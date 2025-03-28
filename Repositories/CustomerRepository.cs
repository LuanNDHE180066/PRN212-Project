using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int CountCustomers()
        {
            _projectContext = new PrnFinalProjectContext();
            return _projectContext.Customers.Count();
        }
    }
}
