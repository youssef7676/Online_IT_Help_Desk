using AutoMapper;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsComments.Commands.CreateTicketCommentsCommands
{
    public class CreateCommentsHandller(ITicketCommentReposatory commrepo, IMapper mapper)
        : IRequestHandler<CreateCommentsCommands, int>
    {
        public async Task<int> Handle(CreateCommentsCommands request, CancellationToken cancellationToken)
        {
            var comment = new TicketComment
            {
                TicketId = request.TicketId,
                Comment = request.Comment,
                UserId = request.CreatedById,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await commrepo.Add(comment);
            await commrepo.Save();

            return comment.Id;
        }
    }
}
