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

namespace IT_Help_Desk.Application.Tickets.Commands.DeleteTicketCommands
{
    public class DeleteTickHandller(ITicketReposatory repo, IMapper mapper)
        : IRequestHandler<DeleteTickCommand, TicketDTO>
    {
        public async Task<TicketDTO> Handle(DeleteTickCommand request, CancellationToken cancellationToken)
        {
            Ticket Deletick = await repo.GetById(request.Id);

            if (Deletick == null)
                return null;


            repo.Delete(Deletick.Id);
            return mapper.Map<TicketDTO>(Deletick);
        }
    }
}
