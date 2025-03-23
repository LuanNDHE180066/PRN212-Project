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
        public void Add(HistoryBuyGood his)
        {
            repo.Add(his);
        }
    }
}
