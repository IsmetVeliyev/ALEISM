using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.Business.Interfaces.IRepository;
using PersonalStudentProject.DataAccess.Models;
using PersonalStudentProject.DataAccess.DTOs.Message;
namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IMessageService
    {
        public Task<Message> SendMessage(SendMessageDto messageDto);
        public Task<List<GetMessageDto>> GetMessagesByRoomId(int roomId);     

        public Task<bool> checkClaimAsync();
    }
}