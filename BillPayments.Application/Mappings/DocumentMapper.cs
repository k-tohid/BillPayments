using BillPayments.Application.DTOs.DocumentDTO;
using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Mappings
{
    public static class DocumentMapper
    {
        public static ReadDocumentDTO ToReadDocumentDTO(this Document document)
        {
            return new ReadDocumentDTO
            {
                Id = document.Id,
                Title = document.Title,
                Description = document.Description,
                CategoryId = document.CategoryId,
                Attachments = document.Attachments,
            };
        }

        public static IEnumerable<ReadDocumentDTO> ToReadDocumentDTO(this IEnumerable<Document> documents)
        {
            return documents.Select(doc =>  ToReadDocumentDTO(doc));
        }
    }
}
