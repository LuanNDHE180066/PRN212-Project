﻿using Repositories;
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
        public Good GetById(int id)
        {
            return goodsRepository.Get(id);
        }
        public void Update(Good good)
        {
            goodsRepository.Update(good);
        }
        public int CountGoods()
        {
            return goodsRepository.CountGoods();
        }
    }
}
