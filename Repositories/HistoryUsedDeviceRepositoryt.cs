using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public class HistoryUsedDeviceRepositoryt
    {

        private PrnFinalProjectContext _context;

        public HistoryUsedDevice GetByInvoiceId(int invoiceId)
        {
            _context = new PrnFinalProjectContext();
            return _context.HistoryUsedDevices.FirstOrDefault(x => x.InvoiceId == invoiceId);
        }
    }
}
