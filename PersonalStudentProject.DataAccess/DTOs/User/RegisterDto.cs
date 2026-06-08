using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalStudentProject.DataAccess.DTOs.User
{ 
    public class RegisterDto

    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }


        public string? Role { get; set; }

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

        [MaxLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; }
    }
}
