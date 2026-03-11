using AutoMapper;
using IT_Help_Desk.Application.TicketsComments.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.Queries.GetById
{
    public class GetCommentsByIdHandller(IMapper mapper, ITicketCommentReposatory repoo)
        : IRequestHandler<GetCommentsByIdQuery, CommentsDTO>
    {
        public async Task<CommentsDTO> Handle(GetCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            TicketComment comm = await repoo.GetById(request.Id);

            if (comm == null)
                return null;

            return mapper.Map<CommentsDTO>(comm);
        }
    }
}
