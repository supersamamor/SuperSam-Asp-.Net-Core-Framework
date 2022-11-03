using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ClientFeedback.Queries;

public record GetClientFeedbackQuery : BaseQuery, IRequest<PagedListResponse<ClientFeedbackState>>;

public class GetClientFeedbackQueryHandler : BaseQueryHandler<ApplicationContext, ClientFeedbackState, GetClientFeedbackQuery>, IRequestHandler<GetClientFeedbackQuery, PagedListResponse<ClientFeedbackState>>
{
    public GetClientFeedbackQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
