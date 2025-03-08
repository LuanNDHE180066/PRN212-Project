using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StaffRepository
    {
        public List<Staff> GetAll()
        {
            return PrnFinalProjectContext.Ins.Staff.Include(x => x.Role).ToList();
        }
    }
}
