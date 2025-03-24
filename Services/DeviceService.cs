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
            return repositories.GetALlDevice()
                ;
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
        public List<DeviceDTO> GetAllDeviceDTO()
        {
            return repositories.GetALlDevice().Select(x => new DeviceDTO
            {
                Did = x.Did,
                Typeid = x.Typeid,
                Hours = x.Hours,
                RunningStatus = x.Running == 1 ? "Running" : "Not Running",
                StatusOfDevice = x.Status == 1 ? "Active" : "Inactive", 
                PricePerHour = x.PricePerHour,
                Type = x.Type
            }).ToList();
        }
        public class DeviceDTO()
        {
            public int Did { get; set; }

            public int? Typeid { get; set; }

            public int? Hours { get; set; }

            public String  RunningStatus { get; set; }

            public String StatusOfDevice { get; set; }

            public decimal? PricePerHour { get; set; }

            public virtual DeviceType? Type { get; set; }
        }


    }
}
