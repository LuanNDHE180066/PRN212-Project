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

        public void Add(HistoryBuyGood hbg)
        {
            repo.Add(hbg);
        }

        public void Update(HistoryBuyGood hbg)
        {
            repo.Update(hbg);
        }

       
    }
}
