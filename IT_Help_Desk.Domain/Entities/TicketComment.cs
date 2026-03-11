using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Entities
{
    public class TicketComment : BaseEntity
    {
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // 1-to-Many → Ticket
        public int TicketId { get; set; }               
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        // 1-to-Many → User
        public int UserId { get; set; }               // اللي كتب الكومنت
        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
