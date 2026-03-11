using AutoMapper;
using IT_Help_Desk.Application.UsersProfiles.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.Commands.CreateProfileCommand
{
    public class CreateUserProfileHandller(IUserProfileReposatory userrepo, IPassword passrepo, IMapper mapper)
        : IRequestHandler<CreateUserProfileCommand, UserProfileDTO>
    {
        public async Task<UserProfileDTO> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var existing = await userrepo.GetByUserId(request.UserId);

            if (existing != null)
                throw new Exception("Profile already exists");

            var profile = new UserProfile
            {
                UserId = request.UserId, 
                Department = request.Data.Department,
                PhoneNumber = request.Data.PhoneNumber,
                JobTitle = request.Data.JobTitle
            };

            await userrepo.Add(profile);
            await userrepo.Save();

            return mapper.Map<UserProfileDTO>(profile);
        }
    }
}
