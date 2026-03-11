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

namespace IT_Help_Desk.Application.UsersProfiles.Queries
{
    public class GetMyProfileHandller(IUserProfileReposatory repo, IMapper mapper)
        : IRequestHandler<GetMyProfileQueries, List<UserProfileDTO>>
    {
        public async Task<List<UserProfileDTO>> Handle(GetMyProfileQueries request, CancellationToken cancellationToken)
        {
            List<UserProfile> profile = await repo.GetAllActiveProfilesAsync();
            List<UserProfileDTO> profiledto = mapper.Map<List<UserProfileDTO>>(profile);
            return profiledto;
        }
    }
}
