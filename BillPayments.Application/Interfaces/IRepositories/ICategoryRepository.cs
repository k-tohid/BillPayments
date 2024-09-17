using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<int> SaveChangesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellation);
        Task CreateCategoryAsync(Category category);
        void UpdateCategoryAsync(Category category);
        void DeleteCategoryAsync(Category category);
    }
}
