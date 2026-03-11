using IT_Help_Desk.Application.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.UsersRigster
{
    public class CreateRigsterCommand : IRequest<UserDTO>
    {
        public RigsterDTO Data { get; set; } = null!;

    }
}
