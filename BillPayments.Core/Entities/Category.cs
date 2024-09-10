using System;
using System.Collections.Generic;
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
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public string? ModifiedById { get; set; }
        public ApplicationUser? ModifiedBy { get; set; }

        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}
