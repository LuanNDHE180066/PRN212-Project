using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    public class HistoryBuyGoodService
    {
        private HistoryBuyGoodRepository repo = new HistoryBuyGoodRepository(); 

        public List<HistoryBuyGood> GetByInvoiceId(int invoiceId)
        {
            return repo.GetByInvoiceId(invoiceId);
        }

        
        public List<HistoryBuyGood> GetByCustomerId(int customerID)
        {
            return repo.GetByCustomerId(customerID);
        }
        public void Add(HistoryBuyGood his)
        {
            repo.Add(his);
        }

        public void Update(HistoryBuyGood hbg)
        {
            repo.Update(hbg);
        }
        public List<Good> GetTop3BestSellingGoods()
        {
            return repo.GetTop3BestSellingGoods();
        }
        public List<HistoryBuyGood> getListGoodByYearAndMonth(int year, int month)
        {
            return repo.getListHistoryByYearAndMonth(year, month);
        }
        public List<HistoryBuyGood> getAllDistinctGood()
        {
            return repo.getAllDistinctGood();
        }
        public void Delete(HistoryBuyGood his)
        {
            repo.Delete(his);
        }
    }
}
