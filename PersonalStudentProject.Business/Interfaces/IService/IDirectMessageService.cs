using PersonalStudentProject.DataAccess.DTOs.DirectMessage;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IDirectMessageService
    {
        Task<DirectMessage> SendMessage(SendDirectMessageDto messageDto);
        Task<List<DirectMessageDto>> GetMessagesByUserId(int userId);
        Task<bool> checkClaimAsync();
    }
}
