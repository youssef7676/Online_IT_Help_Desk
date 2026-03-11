using IT_Help_Desk.Domain.Interfaces;
using IT_Help_Desk.Domain.Entities;
using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace IT_Help_Desk.Application.Tickets.Commands.CreateTicketCommands
{
    public class CreateTicketsHandlers(ITicketReposatory repo, IMapper mapper)
        : IRequestHandler<CreateTicketsCommand, int>

    {
        public async Task<int> Handle(CreateTicketsCommand request, CancellationToken cancellationToken)
        {
            Ticket newticket = mapper.Map<Ticket>(request);
            await repo.Add(newticket);
            await repo.Save();
            return newticket.Id;
        }
    }
}
