using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IDirectMessageRepository
    {
        Task<DirectMessage> SendMessage(DirectMessage message);
        Task<List<DirectMessage>> GetMessagesAsyncByUserId(int userId);
        Task<Dictionary<int, string>> GetUserNamesByIdsAsync(List<int> userIds);
    }
}
