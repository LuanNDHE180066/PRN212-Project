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
    }
}
