using ProjectNamePlaceHolder.Application.Models.Chat;
using ProjectNamePlaceHolder.Application.Responses.Identity;
using ProjectNamePlaceHolder.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Application.Interfaces.Chat;

namespace ProjectNamePlaceHolder.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}