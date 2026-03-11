using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.DTOs
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
    }
}
