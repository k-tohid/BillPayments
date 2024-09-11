using BillPayments.Application.Interfaces.IRepositories;
using BillPayments.Core.Entities;
using BillPayments.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // save to database
        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        // create Category
        public async Task CreateCategoryAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
        }

        // Get Category by Id
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _db.Categories.Include(c => c.Parent).FirstOrDefaultAsync(c => c.Id == id);
        }

        // Get All Categories
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _db.Categories
            .Include(c => c.Parent)
            .ToListAsync();
        }

        // Update Category
        public void UpdateCategoryAsync(Category category)
        {
            _db.Categories.Update(category);
        }

        // Delete Category
        public void DeleteCategoryAsync(Category category)
        {
            _db.Categories.Remove(category);
        }


    }
}
