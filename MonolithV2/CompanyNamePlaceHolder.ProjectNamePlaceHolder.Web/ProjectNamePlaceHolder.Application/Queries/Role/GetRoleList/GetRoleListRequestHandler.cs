using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Application.Models.Role;
using ProjectNamePlaceHolder.Data;

namespace ProjectNamePlaceHolder.Application.Queries.Role.GetRoleList
{
    public class GetRoleListRequestHandler : IRequestHandler<GetRoleListRequest, StaticPagedList<RoleModel>>
    {       
        
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public GetRoleListRequestHandler(ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<StaticPagedList<RoleModel>> Handle(GetRoleListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Roles          
                .AsNoTracking();
                       
            if (request.SearchKey != null) 
            {
                var searchWords = request.SearchKey.ToLower().Split(' ');
                query = query.Where(i => i.Name.ToLower().Contains(searchWords[0]));
                if (searchWords.Length > 1)
                {
                    for (int x = 1; x < searchWords.Length; x++)
                    {
                        var search = searchWords[x];
                        query = query.Where(i => i.Name.ToLower().Contains(search));
                    }
                }            
            }
            switch (request.SortBy) 
            {
                case "Name":
                    if (request.OrderBy == "Asc") {
                        query = query.OrderBy(l=>l.Name);
                    }
                    else {
                        query = query.OrderByDescending(l => l.Name);
                    }
                    break;      
                default:
                    query = query.OrderBy(l => l.Name);
                    break;
            }
            request.PageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;
            if (request.PageSize == 0)
            {
                var recordCount = query.Count();
                request.PageSize = recordCount == 0 ? 1 : recordCount;
            }
            request.PageSize = request.PageSize == 0 ? query.Count() == 0 ? 1 : query.Count() : request.PageSize;
            var pagedRole = query.ToPagedList(request.PageIndex, request.PageSize);
            var userList = _mapper.Map<IList<IdentityRole>, IList<RoleModel>>(await pagedRole.ToListAsync());
            return new StaticPagedList<RoleModel>(userList, pagedRole);        
        }
    }
}
