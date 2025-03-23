using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    public class GoodService
    {
        private GoodRepository _repo = new GoodRepository();

        public List<Good> GetAllActiveGood()
        {
            return _repo.GetAllActiveGood();
        }


        public bool UpdateGood(Good g)
        {
            return _repo.UpdateGood(g);
        }

        public bool AddGood(Good g)
        {
            return _repo.AddGood(g);
        }

    }
}
