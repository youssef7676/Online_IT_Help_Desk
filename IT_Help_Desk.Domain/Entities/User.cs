using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Entities
{
    public class User : BaseEntity                               //Admin ,IT , ده أي حد داخل السيستم: موظف
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }        // Admin, IT, User
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? RefreshToken { get; set; }       // لتخزين التوكين الطويل
        public DateTime? RefreshTokenExpiryTime { get; set; } // وقت انتهاء صلاحية التوكين


        // 1-to-1 → UserProfile
        public UserProfile Profile { get; set; }

        // 1-to-Many → Tickets created by this user
        public ICollection<Ticket> CreatedTickets { get; set; }

        // 1-to-Many → Tickets assigned to this IT user
        public ICollection<Ticket> AssignedTickets { get; set; }

        // 1-to-Many → Comments written by this user
        public ICollection<TicketComment> Comments { get; set; }
    }

}
