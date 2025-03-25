using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GoodsRepository
    {
        public List<Good> GetAll()
        {
            return PrnFinalProjectContext.Ins.Goods.ToList();
        }
        public Good Get(int id)
        {
            return PrnFinalProjectContext.Ins.Goods.Find(id);
        }
        public void Update(Good good)
        {
            PrnFinalProjectContext.Ins.Goods.Update(good);  
            PrnFinalProjectContext.Ins.SaveChanges();
        }
    }
}
