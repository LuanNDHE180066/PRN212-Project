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
        public List<object> GetTop3BestSellingGoods()
        {
            var result = PrnFinalProjectContext.Ins.HistoryBuyGoods
                .GroupBy(d => d.GoodsId)
                .Select(g => new
                {
                    GoodsId = g.Key,
                    TotalSold = g.Sum(d => d.Quantity)
                })
                .OrderByDescending(g => g.TotalSold)
                .Take(3)
                .ToList();

            var topGoods = result
                .Select(r => new
                {
                    Gid = r.GoodsId,
                    GoodsName = PrnFinalProjectContext.Ins.Goods
                                   .Where(g => g.Gid == r.GoodsId)
                                   .Select(g => g.GName)
                                   .FirstOrDefault(),
                    Img = PrnFinalProjectContext.Ins.Goods
                                   .Where(g => g.Gid == r.GoodsId)
                                   .Select(g => g.Img) // Lấy đường dẫn ảnh
                                   .FirstOrDefault(),
                    TotalSold = r.TotalSold
                })
                .ToList<object>();  // Ép kiểu sang List<object>

            return topGoods;
        }
        public List<HistoryBuyGood> getListHistoryByYearAndMonth(int year, int month)
        {
            var result = PrnFinalProjectContext.Ins.HistoryBuyGoods
                .Where(d => d.Date.HasValue && d.Date.Value.Year == year && d.Date.Value.Month == month)
                .Include(h => h.Goods)   
                .Include(h => h.Invoice) 
                .ThenInclude(i => i.Staff) 
                .ToList();

            return result;
        }
        public List<HistoryBuyGood> getAllDistinctGood()
        {
            var result = PrnFinalProjectContext.Ins.HistoryBuyGoods
                .Include(h => h.Goods)
                .Include(h => h.Invoice)
                .ThenInclude(i => i.Staff)
                .ToList();
            return result;

        }

        public void Delete(HistoryBuyGood hbg)
        {
            _context = new();
            _context.HistoryBuyGoods.Remove(hbg);
            _context.SaveChanges();
        }

       
    }
}
