using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Core.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedAt { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public int? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public int? ModifiedById { get; set; }
        public ApplicationUser? ModifiedBy { get; set; }

        public ICollection<string> Attachments { get; set; }
    }
}
