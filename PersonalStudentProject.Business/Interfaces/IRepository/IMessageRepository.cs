using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IMessageRepository
    {
        public Task<Message> SendMessage(Message message);
        public Task<List<Message>> GetMessagesAsyncByRoomId(int roomId);

        
    }
}