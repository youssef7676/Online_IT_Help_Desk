using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.Users.Commands.UserLogin;
using IT_Help_Desk.Application.UsersProfiles.Commands.CreateProfileCommand;
using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.DTOs
{
    public class UserInfoProfile :Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            CreateMap<UserProfile, CreateUserProfileCommand>().ReverseMap();


        }
    }
}
