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

namespace IT_Help_Desk.Application.Users.Queries.GetAllUserById
{
    public class GetUserByIdHandller(IUserRepository repo, IMapper mapper)
        : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await repo.GetByIdWithProfile(request.Id);

            if (user == null)
                return null;

            return mapper.Map<UserDTO>(user);
        }
    }
}
