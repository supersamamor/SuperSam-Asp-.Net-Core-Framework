using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Application.Queries.User.GetUserList
{
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, StaticPagedList<UserModel>>
    {       
        
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public GetUserListRequestHandler(ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<StaticPagedList<UserModel>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.ProjectNamePlaceHolderUser
                .Include(l=>l.Identity)
                .AsNoTracking();
            if (request.SearchKey != null)
            {
                var searchWords = request.SearchKey.ToLower().Split(' ');
                query = query.Where(i => i.FullName.ToLower().Contains(searchWords[0])
                                  || i.Identity.UserName.ToLower().Contains(searchWords[0])
                                  || i.Identity.Email.ToLower().Contains(searchWords[0]));
                if (searchWords.Length > 1)
                {
                    for (int x = 1; x < searchWords.Length; x++)
                    {
                        var search = searchWords[x];
                        query = query.Where(i => i.FullName.ToLower().Contains(search)
                                  || i.Identity.UserName.ToLower().Contains(search)
                                  || i.Identity.Email.ToLower().Contains(search));
                    }
                }            
            }
            switch (request.SortBy)
            {
                case "IdentityUserName":
                    if (request.OrderBy == "Asc") {
                        query = query.OrderBy(l=>l.Identity.UserName);
                    }
                    else {
                        query = query.OrderByDescending(l => l.Identity.UserName);
                    }
                    break;
                case "FullName":
                    if (request.OrderBy == "Asc")
                    {
                        query = query.OrderBy(l => l.FullName);
                    }
                    else
                    {
                        query = query.OrderByDescending(l => l.FullName);
                    }
                    break;
                case "IdentityEmailConfirmed":
                    if (request.OrderBy == "Asc")
                    {
                        query = query.OrderBy(l => l.Identity.EmailConfirmed);
                    }
                    else
                    {
                        query = query.OrderByDescending(l => l.Identity.EmailConfirmed);
                    }
                    break;
                case "IdentityEmail":
                    if (request.OrderBy == "Asc")
                    {
                        query = query.OrderBy(l => l.Identity.Email);
                    }
                    else
                    {
                        query = query.OrderByDescending(l => l.Identity.Email);
                    }
                    break;
                default:
                    query = query.OrderBy(l => l.FullName);
                    break;
            }
            request.PageIndex = request.PageIndex == 0 ? 1 : request.PageIndex;
            if (request.PageSize == 0)
            {
                var recordCount = query.Count();
                request.PageSize = recordCount == 0 ? 1 : recordCount;
            }
            request.PageSize = request.PageSize == 0 ? query.Count() == 0 ? 1 : query.Count() : request.PageSize;
            var pagedUser = query.ToPagedList(request.PageIndex, request.PageSize);
            var userList = _mapper.Map<IList<Data.Models.ProjectNamePlaceHolderUser>, IList<UserModel>>(await pagedUser.ToListAsync());
            return new StaticPagedList<UserModel>(userList, pagedUser);          
        }
    }
}
