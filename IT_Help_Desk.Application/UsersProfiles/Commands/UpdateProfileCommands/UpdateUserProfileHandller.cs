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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IT_Help_Desk.Application.UsersProfiles.Commands.UpdateProfileCommands
{
    public class UpdateUserProfileHandller(IUserProfileReposatory repo, IMapper mapper)
        : IRequestHandler<UpdateUserProfileCommand, UserProfileDTO>
    {
        public async Task<UserProfileDTO> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            UserProfile prof = await repo.GetById(request.UserId);

            if (prof == null)
                return null;

            prof.Department = request.Data.Department;
            prof.PhoneNumber = request.Data.PhoneNumber;
            prof.JobTitle = request.Data.JobTitle;

            repo.Update(prof);
            return mapper.Map<UserProfileDTO>(prof);
        }
    }
}
