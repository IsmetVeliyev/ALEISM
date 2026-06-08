using System;

namespace PersonalStudentProject.DataAccess.DTOs.Message
{
    public class SendMessageResponseDto
    {
        public int Id { get; set; }
        public int SubRoomId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
    }
}
