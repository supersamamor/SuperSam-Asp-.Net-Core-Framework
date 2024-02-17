using AutoMapper;
using ProjectNamePlaceHolder.Infrastructure.Models.Audit;
using ProjectNamePlaceHolder.Application.Responses.Audit;

namespace ProjectNamePlaceHolder.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}