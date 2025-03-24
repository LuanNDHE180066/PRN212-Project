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

        public HistoryUsedDevice GetInvoiceId(Device device)
        {
            return repo.GetInvoiceId(device);   
        }

        public void Add(HistoryUsedDevice hbg)
        {
            repo.Add(hbg);  
        }

        public void Update(HistoryUsedDevice hbg)
        {
            repo.Update(hbg);
        }

        public HistoryUsedDevice GetDeviceRunning(int deviceId)
        {
            return repo.GetDeviceRunning(deviceId);
        }
        public void AddHistoryUsedDevice(HistoryUsedDevice historyUsedDevice)
        {
            repo.AddHistoryUsedDevice(historyUsedDevice);
        }
       
    }
}
