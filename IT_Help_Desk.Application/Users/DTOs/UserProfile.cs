using AutoMapper;
using IT_Help_Desk.Application.TicketsComments.Commands.CreateTicketCommentsCommands;
using IT_Help_Desk.Application.TicketsComments.DTOs;
using IT_Help_Desk.Application.Users.Commands.ChangeRole;
using IT_Help_Desk.Application.Users.Commands.ChangeStatus;
using IT_Help_Desk.Application.Users.Commands.RefreshToken;
using IT_Help_Desk.Application.Users.Commands.UserLogin;
using IT_Help_Desk.Application.Users.Commands.UsersRigster;
using IT_Help_Desk.Application.UsersProfiles.DTOs;
using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Users.DTOs
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<RigsterDTO, User>()
           .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserDTO>()
           .ForMember(dest => dest.Profile,
               opt => opt.MapFrom(src => src.Profile));

            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateLoginCommand>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, ChangeRoleCommand>().ReverseMap();
            CreateMap<User, ChangeUserStatusCommand>().ReverseMap();
            CreateMap<User, RefreshTokenCommand>().ReverseMap();

        }
    }
}
