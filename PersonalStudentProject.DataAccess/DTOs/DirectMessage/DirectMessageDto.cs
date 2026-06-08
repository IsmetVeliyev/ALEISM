namespace PersonalStudentProject.DataAccess.DTOs.DirectMessage
{
    public class DirectMessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }
        public string? SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string? ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
    }
}
