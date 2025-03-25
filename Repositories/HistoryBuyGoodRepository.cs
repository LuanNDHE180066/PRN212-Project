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
        public List<HistoryBuyGood> GetByCustomerId(int customerID)
        {
            _context = new PrnFinalProjectContext();
            return _context.HistoryBuyGoods.Include(x => x.Goods).Include(x => x.Invoice).Where(x => x.Invoice.CustomerId == customerID).ToList();
        }
        public void Add(HistoryBuyGood good)
        {
            _context = new();
            _context.HistoryBuyGoods.Add(good);
            _context.SaveChanges();
        }

        public void Update(HistoryBuyGood hbg)
        {
            _context = new();
            _context.HistoryBuyGoods.Update(hbg);
            _context.SaveChanges();
        }

       
    }
}
