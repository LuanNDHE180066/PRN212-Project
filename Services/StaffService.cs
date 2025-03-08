using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StaffService
    {
        private StaffRepository staffRepository = new StaffRepository();
        public List<Staff> GetAll()
        {
            return staffRepository.GetAll();
        }
        public Staff GetByRoleUsernamePassword(string username, string password, int role)
        {
            return GetAll().Where(s => s.Username.Equals(username) && s.Password.Equals(password) && s.Roleid == role).FirstOrDefault();
        }
        public bool isExistPhone(string phone)
        {
            return this.GetAll().Where(s => s.Phone.Equals(phone)).Any();
        }
        public bool isExistAddress(string address)
        {
            return this.GetAll().Where(s => s.Address.Equals(address)).Any();
        }
        public bool isExistUsername(string username)
        {
            return this.GetAll().Where(s => s.Username.Equals(username)).Any();

        }
        public bool isExistPhoneExceptStaff(string phone,Staff staff)
        {
            return this.GetAll().Where(s => s.Phone.Equals(phone) && !s.Sid.Equals(staff.Sid)).Any();
        }
        public bool isExistAddressExceptStaff(string address, Staff staff)
        {
            return this.GetAll().Where(s => s.Address.Equals(address) && !s.Sid.Equals(staff.Sid)).Any();
        }
        public void AddStaff(Staff staff)
        {
            staffRepository.AddStaff(staff);
        }
        public void UpdateStaff(Staff staff)
        {
            staffRepository.UpdateStaff(staff);
        }
        public Staff GetById(int id)
        {
            return staffRepository.GetById(id);
        }
    }
}
