using IT_Help_Desk.Application.Users.DTOs;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenHandller(IUserRepository repo, IJwtService jwt)
        : IRequestHandler<RefreshTokenCommand, LoginResponseDTO>
    {
        public async Task<LoginResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await repo.GetByRefreshToken(request.RefreshToken);

            if (userEntity == null || userEntity.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null!;

            var accessToken = jwt.GenerateAccessToken(userEntity);

            var newRefreshToken = jwt.GenerateRefreshToken();

            userEntity.RefreshToken = newRefreshToken;
            userEntity.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            repo.Update(userEntity);

            return new LoginResponseDTO
            {
                User = new UserDTO
                {
                    Id = userEntity.Id,
                    FullName = userEntity.FullName,
                    Email = userEntity.Email,
                    Role = userEntity.Role
                },
                Token = accessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = userEntity.RefreshTokenExpiryTime.Value
            };
        }
    }
}
