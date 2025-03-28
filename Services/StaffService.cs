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
        private WorkingHistoryService workingHistoryService = new WorkingHistoryService();
        public List<Staff> GetAll()
        {
            return staffRepository.GetAll();
        }
        public Staff GetByRoleUsernamePassword(string username, string password)
        {
            return GetAll().Where(s => s.Username.Equals(username) && s.Password.Equals(password)).FirstOrDefault();
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
        public bool isExistPhoneExceptStaff(string phone, Staff staff)
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
        public int CountStaff()
        {
            return staffRepository.CountStaff();
        }
        public List<WorkingHistoryDTO> WorkingHistoryDTOs()
        {
            return staffRepository.GetAll().Select(s => new WorkingHistoryDTO()
            {
                Id = s.Sid,
                Name = s.SName,
                Role = s.Role,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Total = workingHistoryService.GetTotalHourById(s.Sid)
            }).ToList();
        }
    }
    public class WorkingHistoryDTO
    {
        //<DataGridTextColumn Width = "*" Header="ID" Binding="{Binding Sid}"></DataGridTextColumn>
        //            <DataGridTextColumn Width = "*" Header="Role" Binding="{Binding Role.Rid}"></DataGridTextColumn>
        //            <DataGridTextColumn Width = "2*" Header="Name" Binding="{Binding SName}"></DataGridTextColumn>
        //            <DataGridTextColumn Width = "*" Header="Start Date" Binding="{Binding StartDate}"></DataGridTextColumn>
        //            <DataGridTextColumn Width = "*" Header="Total Hours"
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public float? Total { get; set; }
    }
}
