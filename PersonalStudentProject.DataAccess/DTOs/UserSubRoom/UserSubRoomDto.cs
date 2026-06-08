using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStudentProject.DataAccess.DTOs.UserSubRoom
{
    public class UserSubRoomDto
    {
        [Required]
        public int SubRoomId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}