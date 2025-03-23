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
    }
}
