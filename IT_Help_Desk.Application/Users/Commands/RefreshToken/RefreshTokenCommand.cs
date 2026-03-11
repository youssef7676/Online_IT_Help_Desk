using IT_Help_Desk.Application.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<LoginResponseDTO>
    {
        public string RefreshToken { get; set; } = null!;

    }
}
