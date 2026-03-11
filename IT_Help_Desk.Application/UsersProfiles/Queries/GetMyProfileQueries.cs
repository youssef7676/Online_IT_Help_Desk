using IT_Help_Desk.Application.UsersProfiles.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.Queries
{
    public class GetMyProfileQueries :IRequest<List<UserProfileDTO>>
    {
        public int UserId { get; set; }
    }
}
