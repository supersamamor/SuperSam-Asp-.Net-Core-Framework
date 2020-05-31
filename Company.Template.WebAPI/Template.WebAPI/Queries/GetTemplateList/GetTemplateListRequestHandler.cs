using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Template.Data;
using Template.WebAPI.Models;
using X.PagedList;

namespace Template.WebAPI.Queries.GetTemplateList
{
    public class GetTemplateListRequestHandler : IRequestHandler<GetTemplateListRequest, CustomPagedList<TemplateModel>>
    {       
        
        private readonly TemplateContext _context;
        private readonly IMapper _mapper;
        public GetTemplateListRequestHandler(TemplateContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<CustomPagedList<TemplateModel>> Handle(GetTemplateListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Template.AsNoTracking();
            if (request.SearchKey != null) {
                query = query.Where(l=>request.SearchKey.Contains(l.Code));
            }
            switch (request.SortBy) {
                case "Code":
                    if (request.OrderBy == "Asc") {
                        query = query.OrderBy(l=>l.Code);
                    }
                    else {
                        query = query.OrderByDescending(l => l.Code);
                    }
                    break;
                case "Name":
                    if (request.OrderBy == "Asc")
                    {
                        query = query.OrderBy(l => l.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(l => l.Name);
                    }
                    break;
            }
            var pagedTemplate = new CustomPagedList<Data.Models.Template>(query, request.PageIndex, request.PageSize); 
            var templateList = _mapper.Map<IList<Data.Models.Template>, IList<TemplateModel>>(await pagedTemplate.Items.ToListAsync());
            return new CustomPagedList<TemplateModel>(templateList, pagedTemplate.PagedListMetaData);
        }
    }
}
