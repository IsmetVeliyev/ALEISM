using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStudentProject.DataAccess.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }

        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public int? ReplyToId { get; set; }

    }
}