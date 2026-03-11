using AutoMapper;
using IT_Help_Desk.Application.Tickets.Commands.CreateTicketCommands;
using IT_Help_Desk.Application.Tickets.DTOs;
using IT_Help_Desk.Application.TicketsAttachments.Commands.AttachmentCommands;
using IT_Help_Desk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.DTOs
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<TicketAttachment, AttachmentDTO>().ReverseMap();

            CreateMap<TicketAttachment, CreateAttachmentCommands>().ReverseMap();
        }
    }
}
