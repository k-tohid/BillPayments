using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Interfaces.IRepositories
{
    public interface IDocumentRepository
    {
        Task<int> SaveChangesAsync();
        Task<Document?> GetDocumentByIdAsync(int id);
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<IEnumerable<Document>> GetAllDocumentsByCategoryIdAsync(int categoryId);
        Task CreateDocumentAsync(Document document);
        void UpdateDocument(Document document);
        void DeleteDocument(Document document);
    }
}
