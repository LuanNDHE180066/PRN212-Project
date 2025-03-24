using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{

    public class InvoiceService
    {
        private InvoiceRepository repo = new();
        private HistoryUsedDeviceRepositoryt hisDeviceRepo= new HistoryUsedDeviceRepositoryt();
        private HistoryBuyGoodRepository goodBuyRepo= new HistoryBuyGoodRepository();
        public List<Invoice> GetAll()
        {
            return repo.GetAll();
        }

        public bool AddNewInvoice(Invoice i)
        {
            return repo.AddNewInvoice(i);
        }


        public Invoice GetById(int id)
        {
            return repo.GetById(id);
        }

        public void Update(Invoice invoice)
        {
            repo.Update(invoice);
        }

        public void UpdateTotal(int id)
        {
            HistoryUsedDevice device = hisDeviceRepo.GetByInvoiceId(id);
            List<HistoryBuyGood> goods = goodBuyRepo.GetByInvoiceId(id).ToList();
            Invoice invoice = this.GetById(id);
            invoice.Total = device.Amount + goods.Sum(s=>s.Amount);
            repo.Update(invoice);
        }
    }
}
