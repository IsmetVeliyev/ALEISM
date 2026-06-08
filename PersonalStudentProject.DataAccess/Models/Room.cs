using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;


namespace PersonalStudentProject.DataAccess.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int userId { get; set; }
        public string RoomName { get; set; }

        public string RoomType { get; set; }

        public bool isPasswordProtected { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; } 
        public DateTime DateUpdated { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }

        public string Location { get; set; }

        public User Owner { get; set; }

        public ICollection<SubRoom> SubRooms { get ; set; }
        public ICollection<Message> Messages { get; set; }


    }
}