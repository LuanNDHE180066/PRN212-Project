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

        public List<Invoice> GetAll()
        {
            return repo.GetAll();
        }

        public bool AddNewInvoice(Invoice i)
        {
            return repo.AddNewInvoice(i);
        }
    }
}
