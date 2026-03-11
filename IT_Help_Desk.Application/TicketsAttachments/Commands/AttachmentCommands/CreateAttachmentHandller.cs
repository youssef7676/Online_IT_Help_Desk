using AutoMapper;
using IT_Help_Desk.Domain.Entities;
using IT_Help_Desk.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.Commands.AttachmentCommands
{
    public class CreateAttachmentHandller(ITicketAttachmentReposatory attrepo, IMapper mapper)
        : IRequestHandler<CreateAttachmentCommands , int>
    {
        public async Task<int> Handle(CreateAttachmentCommands request, CancellationToken cancellationToken)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var storedName = Guid.NewGuid() + Path.GetExtension(request.FileName);
            var fullPath = Path.Combine(folder, storedName);

            await File.WriteAllBytesAsync(fullPath, request.FileData);

            var attachment = new TicketAttachment
            {
                TicketId = request.TicketId,
                FileName = request.FileName,
                FileUrl = $"/attachments/{storedName}",
                UploadedAt = DateTime.UtcNow
            };

            await attrepo.Add(attachment);
            await attrepo.Save();

            return attachment.Id;
        }
    }
}
