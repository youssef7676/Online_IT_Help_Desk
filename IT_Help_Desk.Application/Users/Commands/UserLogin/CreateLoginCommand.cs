using IT_Help_Desk.Application.Users.DTOs;
using MediatR;

namespace IT_Help_Desk.Application.Users.Commands.UserLogin
{
    public class CreateLoginCommand : IRequest<LoginResponseDTO>
    {
        public LoginDTO Data { get; set; } = null!;
    }

    public class LoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
