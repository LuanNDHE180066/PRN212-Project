using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorkingHistoryService
    {
        private WorkingHistoryRepository workingHistoryRepository = new WorkingHistoryRepository();
        public int GetTotalHourById(int sid)
        {
           var total = workingHistoryRepository.GetAll().Where(s => s.Staff.Sid == sid).Sum(s => (s.EndTime.Value.ToTimeSpan() - s.StartTime.Value.ToTimeSpan()).TotalHours);
            return (int)total;
        }
        public List<WorkingHistory> GetByStaffId(int sid)
        {
            return workingHistoryRepository.GetByStaffId(sid);
        }
        public List<WorkingHistory> GetByStaffIdAndDate(int id,DateTime date)
        {
            DateOnly convert = DateOnly.FromDateTime(date);
            return workingHistoryRepository.GetByStaffIdAndDate(id, convert);
        }
    }
}
