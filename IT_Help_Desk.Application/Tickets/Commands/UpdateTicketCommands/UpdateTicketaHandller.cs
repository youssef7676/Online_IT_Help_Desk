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

namespace IT_Help_Desk.Application.Tickets.Commands.UpdateTicketCommands
{
    public class UpdateTicketaHandller(ITicketReposatory tickrepo, IMapper mapper)
        : IRequestHandler<UpdateTicketaCommands, TicketDTO>
    {
        public async Task<TicketDTO> Handle(UpdateTicketaCommands request, CancellationToken cancellationToken)
        {
            Ticket tick = await tickrepo.GetById(request.Id);

            if (tick == null)
                return null;

            tick.Title = request.Title;
            tick.Description = request.Description;

            tickrepo.Update(tick);
            return mapper.Map<TicketDTO>(tick);
        }
    }
}
