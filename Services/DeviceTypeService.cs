using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;
using Repositories;

namespace Services
{
    public class DeviceTypeService
    {
        private DeviceTypeRepositories typeRepositories = new DeviceTypeRepositories();
        public List<DeviceType> GetAllDeviceType()
        {
            return typeRepositories.GetAllDeviceType();
        }
        public void AddDeviceType(DeviceType type)
        {
            typeRepositories.AddDeviceType(type);
        }
        public void UpdateDeviceType(DeviceType type)
        {
            typeRepositories.UpdateDeviceType(type);
        }
        public DeviceType getDeviceTypeByName(string DtName)
        {
            return typeRepositories.getDeviceTypeByName(DtName);
        }
    }
}
