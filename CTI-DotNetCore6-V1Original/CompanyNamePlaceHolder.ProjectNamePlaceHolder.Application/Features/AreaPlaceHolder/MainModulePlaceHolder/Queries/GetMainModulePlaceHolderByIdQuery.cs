using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries
{
    public record GetMainModulePlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class GetMainModulePlaceHolderByIdQueryHandler : BaseAreaPlaceHolderQueryByIdHandler<Core.AreaPlaceHolder.MainModulePlaceHolder, GetMainModulePlaceHolderByIdQuery>, IRequestHandler<GetMainModulePlaceHolderByIdQuery, Option<Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        public GetMainModulePlaceHolderByIdQueryHandler(ApplicationContext context) : base(context) { }
    }
}
