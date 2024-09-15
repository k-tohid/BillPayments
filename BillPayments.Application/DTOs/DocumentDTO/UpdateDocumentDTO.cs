using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.DTOs.DocumentDTO
{
    public class UpdateDocumentDTO
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }


        public ICollection<string> Attachments { get; set; }
    }
}
