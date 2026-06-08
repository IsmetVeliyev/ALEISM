using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.Business.Interfaces.IRepository
{
    public interface IMessageSubRepository
    {
        Task<MessageSub> SendSubMessage(MessageSub messageSub);
        Task<List<MessageSub>> GetSubMessagesAsyncByRoomId(int subRoomId);

    }
}