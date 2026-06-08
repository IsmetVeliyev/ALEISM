using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalStudentProject.DataAccess.DTOs.Room
{
    public class RoomDto
    {        

        public int userId { get; set; }
        [Required(ErrorMessage = "Room name is required")]
        [MaxLength(100, ErrorMessage = "Room name cannot exceed 200 characters")]
        public string RoomName { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [MaxLength(50, ErrorMessage = "Room type cannot exceed 50 characters")]
        public string RoomType { get; set; }

        public bool isPasswordProtected { get; set; }

        public string Password { get; set; }


        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        
        [Required(ErrorMessage = "Date created is required")]
        public DateTime ExpiryDate { get; set; }
        [Required(ErrorMessage = "Capacity is required")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Availability status is required")]
        [MaxLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; }

        
    }
}