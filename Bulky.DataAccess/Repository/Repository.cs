using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;  //internal bcz we want to use it in the derived class
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>(); // here dbSet equivalent to _context.Categories or _context.Products etc hence we can dbSet.Add() or dbSet.Remove() etc 
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList();  //same as _context.Categories.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;  //here query is equivalent to _context.Categories or _context.Products etc
            query=query.Where(filter);
            return query.FirstOrDefault();    //same as Category categoty=_context.Categories.Where(u=>u.Id==id).FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}
