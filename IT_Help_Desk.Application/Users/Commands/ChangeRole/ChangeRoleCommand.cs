using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.ChangeRole
{
    public class ChangeRoleCommand :IRequest<bool>
    {
        public int Id { get; set; }
        public string Role { get; set; }   //  User , IT , Admin
    }
}
