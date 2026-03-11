using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.TicketsComments.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.Queries
{
    public class GetAllCommentsHandller(IMapper mapper, ITicketCommentReposatory tickrepo)
        : IRequestHandler<GetAllCommentsQuery, List<CommentsDTO>>
    {
        public async Task<List<CommentsDTO>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            List<TicketComment> tickcomments = await tickrepo.GetAll();
            List<CommentsDTO> commdto = mapper.Map<List<CommentsDTO>>(tickcomments);
            return commdto;
        }
    }
}
