﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Models;

namespace Services
{
    public class CustomerService
    {
        private CustomerRepository repository = new();
        private CustomerRepositories repositories = new();
        public Customer GetById(int id)
        {
            return repository.GetById(id);
        }

        public List<Customer> getAllCustomer()
        {
            return repositories.getAllCustomer();
        }
        public void AddCustomer(Customer customer)
        {
            repositories.AddCustomer(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            repositories.UpdateCustomer(customer);
        }
        public Customer GetCustomerByID(int id)
        {
            return repositories.GetCustomerByID(id);
        }
        public Customer GetCustomerLogin(string user, string pass)
        {
            return repositories.getAllCustomer().Where(s => s.Username.Equals(user) && s.Password.Equals(pass)).FirstOrDefault();
        }

        public void Update(Customer customer)
        {
            repository.Update(customer);
        }


        public List<CustomerDTO> getAllDTO()
        {
            return repositories.getAllCustomer().Select(x => new CustomerDTO
            {
                Cid = x.Cid,
                CName = x.CName,
                Hours = x.Hours,
                Phone = x.Phone,
                Email = x.Email,
                Username = x.Username,
                Password = x.Password,
                StatusCustomer = x.Status == 1 ? "Active" : "Inactive" // Chuyển đổi Status
            }).ToList();
        }
        public class CustomerDTO()
        {
            public int Cid { get; set; }

            public string? CName { get; set; }

            public int? Hours { get; set; }

            public string? Phone { get; set; }

            public string? Email { get; set; }

            public string? Username { get; set; }

            public string? Password { get; set; }

            public string StatusCustomer { get; set; }
        }
    }
}
