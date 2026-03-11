using IT_Help_Desk.Application.Tickets.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Tickets.Queries
{
    public class GetAllQuery : IRequest<List<TicketDTO>>
    {

    }
}
