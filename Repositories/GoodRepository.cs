using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public class GoodRepository
    {
        private PrnFinalProjectContext _context;

        public List<Good> GetAllActiveGood()
        {
            _context = new();
            return _context.Goods.Where(x => x.Status == 1).ToList();
        }


        public bool AddGood(Good g)
        {
            _context = new();
            _context.Goods.Add(g);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateGood(Good g)
        {
            _context = new();
            _context.Goods.Update(g);
            return _context.SaveChanges() > 0;
        }



    }
}
