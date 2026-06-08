using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalStudentProject.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Location { get; set; }

        public string Role { get; set; }
        public string Password { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ICollection<UserSubRoom> UserSubRooms { get; set; }


    }
}
