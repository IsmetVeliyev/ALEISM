using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonalStudentProject.DataAccess.DTOs.SubRoom
{
    public class SubRoomDto
    {
        [Required(ErrorMessage = "RoomId is required")]

        public int RoomId {get; set;}

        [Required(ErrorMessage = "Room name is required")]
        [MaxLength(100, ErrorMessage = "Room name cannot exceed 50 characters")]
        public string SubRoomName {get; set;}
        
    }
}