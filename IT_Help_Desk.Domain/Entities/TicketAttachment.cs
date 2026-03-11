using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Help_Desk.Domain.Entities
{
    public class TicketAttachment : BaseEntity            //    اضافه مرفقات زى =>   log file , PDFدى زى ال       
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedAt { get; set; }

        // 1-to-Many → Ticket
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
    }

}
