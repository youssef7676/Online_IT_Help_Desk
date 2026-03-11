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

namespace IT_Help_Desk.Application.Tickets.Queries
{
 
    public class GetAllQueryHandler(ITicketReposatory tickrepo, IMapper mapper)
       : IRequestHandler<GetAllQuery, List<TicketDTO>>
    {
        public async Task<List<TicketDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            List<Ticket> tickets = await tickrepo.GetAll();
            List<TicketDTO> tickdto = mapper.Map<List<TicketDTO>>(tickets);
            return tickdto;
        }
    }

}
