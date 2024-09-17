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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _db;

        public DocumentRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }


        public async Task CreateDocumentAsync(Document document)
        {
            await _db.Documents.AddAsync(document);
        }
        

        public async Task<Document?> GetDocumentByIdAsync(int id)
        {
            return await _db.Documents.Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _db.Documents.Include(d => d.Category).AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsByCategoryIdAsync(int categoryId)
        {
            return await _db.Documents.Where(d => d.CategoryId == categoryId).AsSplitQuery().ToListAsync();
        }


        public void UpdateDocument(Document document)
        {
            _db.Documents.Update(document);
        }


        public void DeleteDocument(Document document)
        {
            _db.Documents.Remove(document);
        }

        
    }
}
