using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStudentProject.DataAccess.DTOs.Message
{
    public class GetMessageDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }

        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public int? ReplyToId { get; set; }
        public string ReplyToContent { get; set; }
        public string ReplyToSenderEmail { get; set; }

    }
}