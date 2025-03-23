using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public class DeviceTypeRepositories
    {
        public List<DeviceType> GetAllDeviceType()
        {
            return PrnFinalProjectContext.Ins.DeviceTypes.ToList();
        }
        public void AddDeviceType(DeviceType type)
        {
            PrnFinalProjectContext.Ins.Add(type);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public void UpdateDeviceType(DeviceType type)
        {
            PrnFinalProjectContext.Ins.Update(type);
            PrnFinalProjectContext.Ins.SaveChanges();
        }
        public DeviceType getDeviceTypeByName(string DtName)
        {
            return PrnFinalProjectContext.Ins.DeviceTypes.FirstOrDefault(d => d.DtName == DtName);
        }
    }
}
