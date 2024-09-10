using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Core.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // Foreign keys to ApplicationUser for CreatedBy and ModifiedBy
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public string? ModifiedById { get; set; }
        public ApplicationUser? ModifiedBy { get; set; }

        public ICollection<string> Attachments { get; set; }
    }
}
