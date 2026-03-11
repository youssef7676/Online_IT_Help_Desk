using IT_Help_Desk.Application.UsersProfiles.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.UsersProfiles.Queries.GetProfileById
{
    public class GetProfilesByIdQueries :IRequest<UserProfileDTO>
    {
        public int Id { get; set; }
        public GetProfilesByIdQueries(int id)
        {
            Id = id;
            
        }
    }
}
