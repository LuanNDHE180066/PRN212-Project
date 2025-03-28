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
        public void AddStaff(Staff staff)
        {
            PrnFinalProjectContext.Ins.Staff.Add(staff);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public void UpdateStaff(Staff staff)
        {
            PrnFinalProjectContext.Ins.Staff.Update(staff);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public Staff GetById(int id)
        {
            return PrnFinalProjectContext.Ins.Staff.Find(id);
        }
        public int CountStaff()
        {
            return PrnFinalProjectContext.Ins.Staff.Count();
        }
    }
}
