﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories
{
    public class InvoiceRepository
    {
        private PrnFinalProjectContext _context;
        public List<Invoice> GetAll()
        {
            _context = new PrnFinalProjectContext(); 
            return _context.Invoices.Include(x => x.Staff).Include(x => x.Customer).ToList();
        }

        public bool AddNewInvoice(Invoice i)
        {
            _context = new PrnFinalProjectContext();
            _context.Invoices.Add(i);
           return _context.SaveChanges() > 0;
        }

        public Invoice GetByNull()
        {
            _context = new();
            return _context.Invoices.FirstOrDefault(x => x.Total == 0);
        }

        public Invoice GetById(int id)
        {
            _context = new PrnFinalProjectContext();
            return _context.Invoices.Include(x=> x.Customer).FirstOrDefault(x => x.IId == id);
        }

        public void Update(Invoice invoice)
        {
            _context = new PrnFinalProjectContext();
            _context.Update(invoice);
            _context.SaveChanges();
        }
        
       
    }
}
