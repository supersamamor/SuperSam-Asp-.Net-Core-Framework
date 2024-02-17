using AutoMapper;
using ProjectNamePlaceHolder.Application.Interfaces.Chat;
using ProjectNamePlaceHolder.Application.Models.Chat;
using ProjectNamePlaceHolder.Infrastructure.Models.Identity;

namespace ProjectNamePlaceHolder.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}