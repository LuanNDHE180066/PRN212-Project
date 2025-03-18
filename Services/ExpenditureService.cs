using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExpenditureService
    {
        private ExpenditureRepository expenditureRepository = new ExpenditureRepository();
        public List<Expenditure> GetAll(){
            return expenditureRepository.GetAll();
        }   
        public List<Expenditure> GetByTime(DateTime fromDate, DateTime toDate)
        {
            DateOnly from= DateOnly.FromDateTime(fromDate);
            DateOnly to= DateOnly.FromDateTime(toDate);
            return this.GetAll().Where(s=> s.ExDate.Value>= from && s.ExDate.Value <= to).ToList();
        }
        public List<Expenditure> OrderByTotalAsc(List<Expenditure> list)
        {
            return list.OrderBy(s=>s.Total).ToList();
        }
        public Expenditure GetById(int id)
        {
            return expenditureRepository.GetById(id);
        }
        public void UpdateExpenditure(Expenditure expenditure)
        {
            expenditureRepository.updateExpenditure(expenditure);
        }
    }
}
