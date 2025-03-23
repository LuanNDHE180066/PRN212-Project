using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    public class DeviceService
    {
        private DeviceRepository _repo = new();
        public List<Device> GetAllDevice()
        {
            return _repo.GetAllDevice();
        }

        public Device GetDeviceById(int id)
        {
            return _repo.GetDeviceById(id);
        }

        public void UpdateDevice(Device device)
        {
            _repo.UpdateDevice(device); 
        }


    }
}
