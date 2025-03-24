using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories
{
    public class DeviceRepository
    {
        private PrnFinalProjectContext _context;
        public List<Device> GetAllDevice()
        {
            _context = new();
            return _context.Devices.Include(x => x.Type).ToList();
        }

        //public Device GetDeviceById(int id)
        //{
        //    _context = new();
        //    return _context.Devices.Include(x => x.Type).FirstOrDefault(x => x.Did == id);
        //}

        public void UpdateDevice(Device device)
        {
            _context = new();
             _context.Devices.Update(device);
            _context.SaveChanges();
        }

        public Device GetDeviceById(int id)
        {
            _context = new();
            return _context.Devices.Include(x => x.Type).FirstOrDefault(x => x.Did == id);
        }


    }
}
