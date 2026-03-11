using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.UsersProfiles.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.Queries.GetProfileById
{
    public class GetProfilesByIdHandller(IUserProfileReposatory repo, IMapper mapper)
        : IRequestHandler<GetProfilesByIdQueries, UserProfileDTO>
    {
        public async Task<UserProfileDTO> Handle(GetProfilesByIdQueries request, CancellationToken cancellationToken)
        {
            UserProfile userprof = await repo.GetById(request.Id);

            if (userprof == null)
                return null;

            return mapper.Map<UserProfileDTO>(userprof);
        }
    }
}
