using ProjectNamePlaceHolder.Application.Responses.Identity;
using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Application.Interfaces.Chat;
using ProjectNamePlaceHolder.Application.Models.Chat;

namespace ProjectNamePlaceHolder.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}