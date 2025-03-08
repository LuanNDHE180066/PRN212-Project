using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Staff GetByRoleUsernamePassword(string username,string password,int role)
        {
            return GetAll().Where(s=> s.Username.Equals(username) &&  s.Password.Equals(password) && s.Roleid == role).FirstOrDefault();
        }
    }
}
