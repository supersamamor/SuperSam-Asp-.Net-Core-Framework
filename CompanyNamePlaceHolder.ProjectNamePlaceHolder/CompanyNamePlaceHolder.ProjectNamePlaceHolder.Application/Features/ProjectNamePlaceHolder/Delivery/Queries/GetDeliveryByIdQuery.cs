using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Delivery.Queries;

public record GetDeliveryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DeliveryState>>;

public class GetDeliveryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DeliveryState, GetDeliveryByIdQuery>, IRequestHandler<GetDeliveryByIdQuery, Option<DeliveryState>>
{
    public GetDeliveryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
