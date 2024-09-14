using BillPayments.Application.DTOs.DocumentDTO;
using BillPayments.Core.Entities;
using BillPayments.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Interfaces.IServices
{
    public interface IDocumentService
    {
        Task<ReadDocumentDTO?> GetDocumentByIdAsync(int id);
        Task<IEnumerable<ReadDocumentDTO>> GetAllDocumentsAsync();
        Task<IEnumerable<ReadDocumentDTO>> GetAllDocumentsByCategoryIdAsync(int categoryId);
        Task<ServiceResult> CreateDocument(CreateDocumentDTO dto);
        Task<ServiceResult> UpdateDocumentAsync(UpdateDocumentDTO dto);
        Task<ServiceResult> DeleteDocumentAsync(int id);
    }
}
