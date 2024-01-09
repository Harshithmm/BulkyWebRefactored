﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Data;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
