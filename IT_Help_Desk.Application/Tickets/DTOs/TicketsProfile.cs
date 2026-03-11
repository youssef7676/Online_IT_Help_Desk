using AutoMapper;
using IT_Help_Desk.Application.Tickets.Commands.CreateTicketCommands;
using IT_Help_Desk.Application.Tickets.Commands.DeleteTicketCommands;
using IT_Help_Desk.Application.Tickets.Commands.UpdateTicketCommands;
using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.Tickets.DTOs
{
    public class TicketsProfile : Profile
    {
        public TicketsProfile()
        {
            CreateMap<Ticket, TicketDTO>().ReverseMap();
            CreateMap<Ticket , CreateTicketsCommand>().ReverseMap();
            CreateMap<Ticket, UpdateTicketaCommands>().ReverseMap();
            CreateMap<Ticket, DeleteTickCommand>().ReverseMap();


        }
    }
}
