using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    
    public class GoodTypeService
    {
        private GoodTypeRepository _repo = new GoodTypeRepository();

        public List<GoodType> GetGoodTypes()
        {
            return _repo.GetAll();
        }

        public bool AddGoodType(GoodType gt)
        {
            return _repo.UpdateGoodType(gt);
        }

        public bool UpdateGoodType(GoodType gt)
        {
            return _repo.UpdateGoodType(gt);
        }
    }
}
