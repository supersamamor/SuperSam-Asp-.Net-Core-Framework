using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Data;
using Identity.WebAPI.Models;
using X.PagedList;
using Microsoft.AspNetCore.Identity;

namespace Identity.WebAPI.Queries.GetRoleList
{
    public class GetRoleListRequestHandler : IRequestHandler<GetRoleListRequest, CustomPagedList<RoleModel>>
    {       
        
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        public GetRoleListRequestHandler(IdentityContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<CustomPagedList<RoleModel>> Handle(GetRoleListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Roles          
                .AsNoTracking();

            if (request.FilterBy == "CurrentRoles")
            {
                if (request.UserId != 0)
                {
                    query = from r in query
                            join ur in _context.UserRoles on r.Id equals ur.RoleId
                            join au in _context.ProjectNamePlaceHolderUser on ur.UserId equals au.Identity.Id
                            where au.Id == request.UserId
                            select r;
                }
            }
            else if(request.FilterBy == "AvailableRoles")
            {
                if (request.UserId != 0)
                {
                    var currentRoles = await (from r in query
                            join ur in _context.UserRoles on r.Id equals ur.RoleId
                            join au in _context.ProjectNamePlaceHolderUser on ur.UserId equals au.Identity.Id
                            where au.Id == request.UserId
                            select r.Id).ToListAsync();

                    query = from r in query
                                 where !currentRoles.Contains(r.Id)
                                 select r;
                }            
            }         

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
            var pagedRole = new CustomPagedList<IdentityRole>(query, request.PageIndex, request.PageSize); 
            var userList = _mapper.Map<IList<IdentityRole>, IList<RoleModel>>(await pagedRole.Items.ToListAsync());
            return new CustomPagedList<RoleModel>(userList, pagedRole.PagedListMetaData);
        }
    }
}
