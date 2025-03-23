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


    }
}
