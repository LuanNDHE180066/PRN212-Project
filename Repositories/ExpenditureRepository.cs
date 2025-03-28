using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ExpenditureRepository
    {
        public List<Expenditure> GetAll()
        {
            return PrnFinalProjectContext.Ins.Expenditures.Include(s=>s.Staff).Include(s=> s.Goods).ToList();
        }
        public Expenditure GetById(int id)
        {
            return PrnFinalProjectContext.Ins.Expenditures.Include(s => s.Staff).Include(s => s.Goods).Where(s=>s.ExId==id).First();
        }
        public void updateExpenditure(Expenditure expenditure)
        {
            PrnFinalProjectContext.Ins.Expenditures.Update(expenditure);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public void AddExpenditure(Expenditure expenditure)
        {
            PrnFinalProjectContext.Ins.Expenditures.Add(expenditure);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public decimal? GetTotalByMonth()
        {
            return PrnFinalProjectContext.Ins.Expenditures. Sum(e => e.Total);
        }
        public decimal? GetTotalByYearAndMonth(int year, int month)
        {
            return PrnFinalProjectContext.Ins.Expenditures
                .Where(i => i.ExDate.HasValue &&
                            i.ExDate.Value.Year == year &&
                            i.ExDate.Value.Month == month)
                .Sum(i => (decimal?)i.Total) ?? 0;
        }
        public List<Expenditure> getListHistoryExpenditureByYearAndMonth(int year, int month)
        {
            var result = PrnFinalProjectContext.Ins.Expenditures
                .Where(d => d.ExDate.HasValue && d.ExDate.Value.Year == year && d.ExDate.Value.Month == month)
                .Include(h => h.Goods)
                .Include(h => h.Staff) 
                .ToList();

            return result;
        }

    }
}
