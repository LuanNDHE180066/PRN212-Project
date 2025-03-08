using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoleRepository
    {
        public List<Role> GetAll()
        {
            return PrnFinalProjectContext.Ins.Roles.ToList();
        }
    }
}
