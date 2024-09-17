using BillPayments.Application.DTOs.DocumentDTO;
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
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;


        public DocumentService(
            IDocumentRepository documentRepository, 
            IHttpContextAccessor httpContextAccessor, 
            ICategoryService categoryService)
        {
            _documentRepository = documentRepository;
            _httpContextAccessor = httpContextAccessor;
            _categoryService = categoryService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new Exception("User is not authenticated");

            return int.Parse(userIdClaim.Value);
        }


        public async Task<ServiceResult> CreateDocument(CreateDocumentDTO dto)
        {
            var category = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);
            if (category == null)
            {
                return ServiceResult.Failure("The specified category does not exist.");
            }

            var currentUser = GetCurrentUserId();

            var document = new Document()
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                CreatedOn = DateTime.UtcNow,
                CreatedById = currentUser,
                Attachments = dto.Attachments,
            };

            await _documentRepository.CreateDocumentAsync(document);
            
            var createResult = await _documentRepository.SaveChangesAsync();

            if (createResult > 0)
            {
                return ServiceResult.Success();
            }

            return ServiceResult.Failure("Something went wrong creating document");
        }



        public async Task<IEnumerable<ReadDocumentDTO>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllDocumentsAsync();
            return documents.ToReadDocumentDTO();
        }


        public async Task<IEnumerable<ReadDocumentDTO>> GetAllDocumentsByCategoryIdAsync(int categoryId)
        {
            var documents = await _documentRepository.GetAllDocumentsByCategoryIdAsync(categoryId);
            return documents.ToReadDocumentDTO();
        }


        public async Task<ReadDocumentDTO?> GetDocumentByIdAsync(int id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);

            if (document == null) return null;

            return document.ToReadDocumentDTO();
        }


        public async Task<ServiceResult> UpdateDocumentAsync(UpdateDocumentDTO dto)
        {
            var currentUser = GetCurrentUserId();

            var document = await _documentRepository.GetDocumentByIdAsync(dto.Id);

            if (document == null)
                return ServiceResult.Failure("No such document exist");


            var updtedDocument = new Document()
            {
                Id = dto.Id,
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                Description = dto.Description,
                Attachments = dto.Attachments,
                ModifiedAt = DateTime.UtcNow,
                ModifiedById = currentUser
            };

            _documentRepository.UpdateDocument(updtedDocument);

            var updateResult = await _documentRepository.SaveChangesAsync();

            if (updateResult > 0)
                return ServiceResult.Success();


            return ServiceResult.Failure("Something went wrong updating document");

        }


        public async Task<ServiceResult> DeleteDocumentAsync(int id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);

            if (document == null)
                return ServiceResult.Failure("No such Document Exist");

            _documentRepository.DeleteDocument(document);
            
            var deleteResult = await _documentRepository.SaveChangesAsync();

            if (deleteResult > 0)
                return ServiceResult.Success();

            return ServiceResult.Failure("Something went wrong deleting Document");
        }

        
    }
}
