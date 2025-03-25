using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public HistoryUsedDevice GetInvoiceId(Device device)
        {
            _context = new();
            return _context.HistoryUsedDevices.FirstOrDefault(x => x.DeviceId == device.Did && x.End == null);
        }

        public void Add(HistoryUsedDevice hbg)
        {
            _context = new();
            _context.Add(hbg);
            _context.SaveChanges();
        }

        public void Update(HistoryUsedDevice hbg)
        {
            _context = new();
            _context.Update(hbg);
            _context.SaveChanges();
        }

        public HistoryUsedDevice GetDeviceRunning(int deviceId)
        {
            _context = new();
            List<int?> listIdByInvoiceId = _context.HistoryUsedDevices.Where(x => x.DeviceId == deviceId).Select(x => x.InvoiceId).ToList();

            Device statuseDevice = _context.Devices.FirstOrDefault(x => x.Did == deviceId && x.Status == 2);

            return _context.HistoryUsedDevices.Include(x => x.Device).FirstOrDefault(x => x.DeviceId == deviceId && x.InvoiceId == listIdByInvoiceId.Max() && statuseDevice != null);
        }


        public void AddHistoryUsedDevice(HistoryUsedDevice device)
        {
            _context = new PrnFinalProjectContext();
            _context.HistoryUsedDevices.Add(device);
            _context.SaveChanges();
        }

    }
}
