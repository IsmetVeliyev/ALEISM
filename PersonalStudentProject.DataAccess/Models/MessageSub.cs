using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStudentProject.DataAccess.Models
{
    public class MessageSub
    {
        public int id {get;set;}
        public int subRoomId {get;set;}

        public string senderEmail {get;set;}

        public string content {get; set;}

        public DateTime DateSent { get; set; }

        public int? ReplyToId { get; set; }

    }
}