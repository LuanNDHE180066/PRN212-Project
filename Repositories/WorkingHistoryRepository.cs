using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class WorkingHistoryRepository
    {
        public List<WorkingHistory> GetAll()
        {
            return PrnFinalProjectContext.Ins.WorkingHistories.Include(s=>s.Staff).ToList();
        }
        public List<WorkingHistory> GetByStaffId(int id)
        {
            return PrnFinalProjectContext.Ins.WorkingHistories.Where(s=>s.Staff.Sid== id).ToList();
        }
        public List<WorkingHistory> GetByStaffIdAndDate(int id, DateOnly? date)
        {
            return PrnFinalProjectContext.Ins.WorkingHistories.Where(s => s.Staff.Sid == id && s.Date.Equals(date)).ToList();
        }
    }
}
