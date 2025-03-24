using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories
{
    public class DeviceRepositories
    {
            public List<Device> GetALlDevice() { 
                return PrnFinalProjectContext.Ins.Devices.Include(d => d.Type).ToList();
            }
        public void UpdateDevice(Device device)
        {
            PrnFinalProjectContext.Ins.Update(device);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public void AddDevice(Device device)
        {
            PrnFinalProjectContext.Ins.Add(device);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public Device GetDeviceByID(int id)
        {
            return PrnFinalProjectContext.Ins.Devices.Find(id);
        }
    }
}
