using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }

        // 1-to-1 → User
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
