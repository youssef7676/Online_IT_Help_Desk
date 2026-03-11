using IT_Help_Desk.Application.TicketsComments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.Queries
{
    public class GetAllCommentsQuery : IRequest<List<CommentsDTO>>
    {


    }
}
