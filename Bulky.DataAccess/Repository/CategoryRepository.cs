
using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Data;
using BulkyWeb.Models;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
               _context=context; 
        }
        public void Update(Category category)
        {
            _context.Update(category);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
