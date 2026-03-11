using AutoMapper;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdHandller(ITicketReposatory repo, IMapper mapper)
        : IRequestHandler<GetTicketByIdQuery, TicketDTO>
    {
        public async Task<TicketDTO> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            Ticket tick = await repo.GetById(request.Id);

            if (tick == null)
                return null;

            return mapper.Map<TicketDTO>(tick);
        }
    }
}
