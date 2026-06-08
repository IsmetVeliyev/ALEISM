namespace PersonalStudentProject.DataAccess.Models
{
    
    public class UserSubRoom
    {
        public int UserId { get; set; }

        public int SubRoomId { get; set; }

        public User User { get; set; }

        public SubRoom SubRoom { get; set; }
    }
}