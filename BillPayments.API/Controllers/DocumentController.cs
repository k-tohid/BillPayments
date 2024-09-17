using BillPayments.Application.DTOs.DocumentDTO;
using BillPayments.Application.Interfaces.IServices;
using BillPayments.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillPayments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ICategoryService _categoryService;

        public DocumentController(IDocumentService documentService, ICategoryService categoryService)
        {
            _documentService = documentService;
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int documentId)
        {
            var documnt = await _documentService.GetDocumentByIdAsync(documentId);

            if (documnt == null)
                return NotFound();

            return Ok(documnt);
        }


        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetDocumentsByCategoryId(int categoryId)
        {
            var documents = await _documentService.GetAllDocumentsByCategoryIdAsync(categoryId);

            if (documents == null)
                return NotFound();

            return Ok(documents);
        }


        [HttpPost]
        public async Task<IActionResult> PostDocument(CreateDocumentDTO dto)
        {
            var categoryExist = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);

            if (categoryExist == null)
                return Problem("No category exists with that id");
            
            var attachmentFileNames = new List<string>();

            if (dto.AttachFiles != null && dto.AttachFiles.Any())
            {
                foreach (var file in dto.AttachFiles)
                {
                    if (file.Length <= 0)
                        continue;

                    var fileName = file.FileName;
                    attachmentFileNames.Add(fileName);

                    var filePath = Path.Combine("wwwroot", "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            await _documentService.CreateDocument(new CreateDocumentDTO
            {
                Title = dto.Title,
                Description = dto.Description,
                Attachments = attachmentFileNames,
                CategoryId = dto.CategoryId,
            });

            return Ok("New Document Created.");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);

            if (document == null) return NotFound();

            foreach (var file in document.Attachments)
            {
                var filePath = Path.Combine("wwwroot", "uploads", file);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            await _documentService.DeleteDocumentAsync(id);

            return NoContent();

        }
    }
}
