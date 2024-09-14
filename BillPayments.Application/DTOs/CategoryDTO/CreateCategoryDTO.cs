using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.DTOs.CategoryDTO
{
    public class CreateCategoryDTO
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string? Thumbnail { get; set; }


        [ForeignKey(nameof(Category))]
        public int? ParentId { get; set; }

    }
}
