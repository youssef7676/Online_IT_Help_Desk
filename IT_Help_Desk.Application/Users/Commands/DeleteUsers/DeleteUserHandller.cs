using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.Users.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.Commands.DeleteUsers
{
    public class DeleteUserHandller(IUserRepository repo, IMapper mapper)
        : IRequestHandler<DeleteUserCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repo.GetByIdWithProfile(request.Id);

            if (user == null)
                return null;

            // Soft delete user
            user.IsDeleted = true;

            // Soft delete profile
            if (user.Profile != null)
                user.Profile.IsDeleted = true;

            await repo.Save(); 

            return mapper.Map<UserDTO>(user);
        }
    }
}
