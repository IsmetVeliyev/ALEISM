using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;


namespace PersonalStudentProject.DataAccess.Models
{
    public class SubRoom
    {
        public int Id {get; set;}

        public int RoomId {get; set;}

        public string SubRoomName {get; set;}

        public Room room {get; set;}

        public ICollection<UserSubRoom> UserSubRooms { get; set; }


    }
}