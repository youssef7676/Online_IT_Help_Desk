using IT_Help_Desk.Application.Tickets.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<TicketDTO>
    {
        public int Id { get; set; }
        public GetTicketByIdQuery(int id)
        {
            Id = id;
        }
    }
}
