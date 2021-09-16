using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder
{
    public class BaseAreaPlaceHolderQueryHandler<TEntity, TQuery> : BaseQueryHandler<ApplicationContext, TEntity, TQuery> where TEntity : BaseEntity where TQuery : BaseQuery
    {
        public BaseAreaPlaceHolderQueryHandler(ApplicationContext context) : base(context)
        {
        }
    }
}
