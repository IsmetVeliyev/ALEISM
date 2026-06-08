namespace PersonalStudentProject.DataAccess.Models
{
    public class DirectMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
    }
}
