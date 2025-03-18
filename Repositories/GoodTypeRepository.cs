using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public class GoodTypeRepository
    {
        private PrnFinalProjectContext _context;

        public List<GoodType> GetAll()
        {
            _context = new PrnFinalProjectContext();
            return _context.GoodTypes.ToList();
        }

        public bool AddGoodType(GoodType gt)
        {
            _context = new PrnFinalProjectContext();
            _context.GoodTypes.Add(gt);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateGoodType(GoodType gt)
        {
            _context = new PrnFinalProjectContext();
            _context.GoodTypes.Update(gt);
            return _context.SaveChanges() > 0;
        }
    }
}
