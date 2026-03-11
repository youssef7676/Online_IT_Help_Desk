using AutoMapper;
using IT_Help_Desk.Application.Tickets.Commands.CreateTicketCommands;
using IT_Help_Desk.Application.TicketsComments.Commands.CreateTicketCommentsCommands;
using IT_Help_Desk.Application.TicketsComments.Queries;
using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.DTOs
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<TicketComment, CreateCommentsCommands>().ReverseMap();
            CreateMap<TicketComment, CommentsDTO>().ReverseMap();



        }
    }
}
