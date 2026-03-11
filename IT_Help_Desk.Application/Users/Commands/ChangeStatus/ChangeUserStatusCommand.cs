using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.ChangeStatus
{
    public class ChangeUserStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
