using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // Foreign keys to ApplicationUser for CreatedBy and ModifiedBy
        [ForeignKey(nameof(ApplicationUser))]
        public int? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public int? ModifiedById { get; set; }
        public ApplicationUser? ModifiedBy { get; set; }


        [ForeignKey(nameof(Category))]
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}
