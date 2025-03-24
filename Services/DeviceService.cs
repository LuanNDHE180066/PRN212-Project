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
        private DeviceRepositories repositories = new DeviceRepositories();
        public List<Device> GetAllDevice()
        {
            return repositories.GetALlDevice();
        }
        public void AddDevice(Device device)
        {
             repositories.AddDevice(device);
        }
        public void UpdateDevice(Device device)
        {
            repositories.UpdateDevice(device);
        }
        public Device getDeviceByID(int id)
        {
            return repositories.GetDeviceByID(id);
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
