using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Entities
{
    public class Ticket : BaseEntity                       //(طلب الدعم)
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public TicketStatus Status { get; set; }       //   // Pending, InProgress, Closed, Rejected (enumبتاع ال )

        // CreatedBy → mandatory
        public int CreatedById { get; set; }                 //  صاحب الطلب
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }                  //IT موظف ال

        // AssignedTo → optional
        public int? AssignedToId { get; set; }
        [ForeignKey("AssignedToId")]
        public User AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        // 1-to-Many → Comments
        public ICollection<TicketComment> Comments { get; set; }

        // 1-to-Many → Attachments
        public ICollection<TicketAttachment> Attachments { get; set; }
    }

}
