using IT_Help_Desk.Application.UsersProfiles.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.Commands.CreateProfileCommand
{
    public class CreateUserProfileCommand :IRequest<UserProfileDTO>
    {
        public int UserId { get; set; }
        public UserProfileDTO Data { get; set; }
    }
}
