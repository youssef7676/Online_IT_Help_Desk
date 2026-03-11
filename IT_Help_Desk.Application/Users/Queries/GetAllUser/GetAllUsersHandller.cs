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

namespace IT_Help_Desk.Application.Users.Queries.GetAllUser
{
    public class GetAllUsersHandller(IUserRepository userrepo, IMapper mapper)
        : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await userrepo.GetAllUserWithProfile(u => u.Profile);
            List<UserDTO> userdto = mapper.Map<List<UserDTO>>(users);
            return userdto;
        }
    }
}
