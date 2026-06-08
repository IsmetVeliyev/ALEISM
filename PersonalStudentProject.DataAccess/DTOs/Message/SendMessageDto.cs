using System.ComponentModel.DataAnnotations;

namespace PersonalStudentProject.DataAccess.DTOs.Message
{
    public class SendMessageDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RoomId must be a positive number.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }

        public int? ReplyToId { get; set; }
    }
}