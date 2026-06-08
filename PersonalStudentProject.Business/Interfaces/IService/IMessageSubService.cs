using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalStudentProject.DataAccess.DTOs.Message;

namespace PersonalStudentProject.Business.Interfaces.IService
{
    public interface IMessageSubService
    {
        Task<SendMessageResponseDto> SendSubMessage(SendMessageDto messageDto);

        Task<List<GetMessageDto>> GetSubMessagesByRoomId(int subRoomId);

        Task<bool> checkClaimAsync();

    }
}