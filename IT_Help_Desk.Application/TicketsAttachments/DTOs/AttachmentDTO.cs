using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Application.TicketsAttachments.DTOs
{
    public class AttachmentDTO
    {
        public int TicketId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        //public string FullUrl => FileUrl;

        public DateTime UploadedAt { get; set; }

    }
}
