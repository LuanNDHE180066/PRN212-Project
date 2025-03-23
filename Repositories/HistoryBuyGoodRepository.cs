using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories
{


    public class HistoryBuyGoodRepository
    {
        private PrnFinalProjectContext _context;

        public List<HistoryBuyGood> GetByInvoiceId(int invoiceId)
        {
            _context = new PrnFinalProjectContext();
            return _context.HistoryBuyGoods.Include(x=> x.Goods).Where(x => x.InvoiceId == invoiceId).ToList();
        }
    }
}
