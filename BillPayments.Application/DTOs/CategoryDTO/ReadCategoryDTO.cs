using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.DTOs.CategoryDTO
{
    public class ReadCategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string? Thumbnail { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<ReadCategoryDTO> SubCategories { get; set; }
    }
}
