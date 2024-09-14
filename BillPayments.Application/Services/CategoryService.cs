using BillPayments.Application.DTOs.CategoryDTO;
using BillPayments.Application.Interfaces.IRepositories;
using BillPayments.Application.Interfaces.IServices;
using BillPayments.Application.Mappings;
using BillPayments.Core.Entities;
using BillPayments.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new Exception("User is not authenticated");

            return int.Parse(userIdClaim.Value);
        }



        public async Task<ServiceResult> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var currentUserId = GetCurrentUserId();

            var category = new Category()
            {
                Title = dto.Title,
                IsActive = dto.IsActive,
                Thumbnail = dto.Thumbnail,
                CreatedOn = DateTime.UtcNow,
                CreatedById = currentUserId,
                ParentId = dto.ParentId
            };

            await _categoryRepository.CreateCategoryAsync(category);

            var createResult = await _categoryRepository.SaveChangesAsync();

            if (createResult >  0)
            {
                return ServiceResult.Success();
            }

            return ServiceResult.Failure("Unknown error occured");
        }


        public async Task<ReadCategoryDTO?> GetCategoryByIdAsync(int id)
        {

            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            return category.ToReadCategoryDTO();

        }


        public async Task<IEnumerable<ReadCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.ToReadCategoryDTO();
        }


        public async Task<ServiceResult> UpdateCategoryAsync(UpdateCategoryDTO dto)
        {
            var currentUserId = GetCurrentUserId();

            var category = new Category()
            {
                Id = dto.Id,
                Title = dto.Title,
                IsActive = dto.IsActive,
                Thumbnail = dto.Thumbnail,
                ModifiedById = currentUserId,
                ParentId = dto.ParentId,
                ModifiedAt = DateTime.UtcNow,
            };

            _categoryRepository.UpdateCategoryAsync(category);

            var updateResult = await _categoryRepository.SaveChangesAsync();

            if (updateResult > 0)
            {
                return ServiceResult.Success();
            }

            return ServiceResult.Failure("Unknown error Ocuured");
        }

        public async Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return ServiceResult.Failure("The category with that id doesnt exist");
            }

            _categoryRepository.DeleteCategoryAsync(category);

            var deleteResult = await _categoryRepository.SaveChangesAsync();

            if (deleteResult > 0)
            {
                return ServiceResult.Success();
            }

            return ServiceResult.Failure("Unknown Error ocuured");
        }

        
    }
}
