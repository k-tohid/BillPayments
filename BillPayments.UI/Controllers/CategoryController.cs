using BillPayments.Application.DTOs.CategoryDTO;
using BillPayments.Application.Interfaces.IServices;
using BillPayments.Application.Services;
using BillPayments.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BillPayments.UI.Controllers
{
    public class CategoryController : Controller
    {

        //private readonly IHttpClientFactory _httpClientFactory;


        //public CategoryController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        private readonly HttpClient _client;

        
        public CategoryController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ApiClient");
        }


        [Route("category/")]
        [Route("category/list")]
        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            
            var response = await _client.GetAsync("/api/category");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<IEnumerable<ReadCategoryDTO>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(categories);
            }

            return View(Enumerable.Empty<ReadCategoryDTO>());

        }


        [HttpGet]
        public async Task<IActionResult> LoadSubCategories([FromQuery]int categoryId)
        {
            var response = await _client.GetAsync($"/api/category/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var category = JsonSerializer.Deserialize<ReadCategoryDTO>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (category == null || !category.SubCategories.Any())
                {
                    return Content("");
                }
                return PartialView("TreePartial", category.SubCategories);
            }

            return Content("Error loading subcategories.");
        }

    }
}
