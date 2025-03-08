using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService
    {
        private RoleRepository roleRepository = new RoleRepository();
        public List<Role> GetAll()
        {
            return roleRepository.GetAll();
        }
    }
}
