using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    public class HistoryUsedDeviceService
    {
        private HistoryUsedDeviceRepositoryt repo = new HistoryUsedDeviceRepositoryt();

        public HistoryUsedDevice GetByInvoiceId(int invoiceId)
        {
            return repo.GetByInvoiceId(invoiceId);
        }
    }
}
