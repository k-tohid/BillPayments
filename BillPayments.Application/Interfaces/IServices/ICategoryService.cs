using BillPayments.Application.DTOs.CategoryDTO;
using BillPayments.Core.Entities;
using BillPayments.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<ServiceResult> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<ReadCategoryDTO?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<ReadCategoryDTO>> GetAllCategoriesAsync();
        Task<ServiceResult> UpdateCategoryAsync(UpdateCategoryDTO dto);
        Task<ServiceResult> DeleteCategoryAsync(int id);

    }
}
