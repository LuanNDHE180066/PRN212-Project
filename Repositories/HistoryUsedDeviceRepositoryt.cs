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
            return _context.HistoryUsedDevices.FirstOrDefault(x => x.DeviceId == deviceId && x.End == null);
        }

       
        public void AddHistoryUsedDevice(HistoryUsedDevice device)
        {
            _context = new PrnFinalProjectContext();
            _context.HistoryUsedDevices.Add(device);
            _context.SaveChanges();
        }
        public List<HistoryUsedDevice> GetHistoryUsedDevices()
        {
            var result = PrnFinalProjectContext.Ins.HistoryUsedDevices
            .Include(h => h.Device)
             .ThenInclude(d => d.Type) 
             .ToList();

            return result;
        }
        public List<HistoryUsedDevice> getListHistoryUsedDeviceByYearAndMonth(int year, int month)
        {
            var result = PrnFinalProjectContext.Ins.HistoryUsedDevices
                .Where(d => d.Date.HasValue && d.Date.Value.Year == year && d.Date.Value.Month == month)
                .Include(h => h.Device) 
                .ToList();

            return result;
        }

    }
}
