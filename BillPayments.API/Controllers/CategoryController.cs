using BillPayments.Application.DTOs.CategoryDTO;
using BillPayments.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillPayments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }


        [HttpPost]
        public async Task<IActionResult> PostCategory(CreateCategoryDTO dto)
        {
            var result = await _categoryService.CreateCategoryAsync(dto);

            if (result.IsSuccess)
            {
                return Ok("Category is created.");
            }

            return Problem(title: "Category creation failed", detail: result.ErrorMessage);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PutCategory(UpdateCategoryDTO dto)
        {
            var result = await _categoryService.UpdateCategoryAsync(dto);

            if (result.IsSuccess)
            {
                return Ok("Category is updated.");
            }

            return Problem(title: "Category update failed", detail: result.ErrorMessage);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return Problem(title: "Delete Category", detail: result.ErrorMessage);
        }
    }
}
