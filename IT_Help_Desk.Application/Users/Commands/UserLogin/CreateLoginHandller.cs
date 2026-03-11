using AutoMapper;
using IT_Help_Desk.Application.JwtService;
using IT_Help_Desk.Application.Users.DTOs;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.UserLogin
{
    public class CreateLoginHandller(IUserRepository repo, IPassword pass, IMapper mapper, JwtTokens jwtService)
        : IRequestHandler<CreateLoginCommand, LoginResponseDTO>
    {
        public async Task<LoginResponseDTO> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await repo.GetByEmailWithProfile(request.Data.Email);
            if (user == null)
                throw new Exception("Invalid Email or Password");

            var isValid = pass.Verify(request.Data.Password, user.PasswordHash);
            if (!isValid)
                throw new Exception("Invalid Email or Password");

            if (!user.IsActive)
                throw new Exception("User is deactivated");

            var token = jwtService.GenerateToken(user);

            var userDto = mapper.Map<UserDTO>(user);

            return new LoginResponseDTO
            {
                User = userDto,
                Token = token
            };


        }
    }
}
