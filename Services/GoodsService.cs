using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GoodsService
    {
        private GoodsRepository goodsRepository = new GoodsRepository();
        public List<Good> GetAll()
        {
            return goodsRepository.GetAll();
        }
    }
}
