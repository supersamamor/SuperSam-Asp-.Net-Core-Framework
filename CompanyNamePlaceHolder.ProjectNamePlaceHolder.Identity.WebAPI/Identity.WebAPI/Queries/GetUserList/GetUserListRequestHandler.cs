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

namespace Identity.WebAPI.Queries.GetUserList
{
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, CustomPagedList<UserModel>>
    {       
        
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        public GetUserListRequestHandler(IdentityContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<CustomPagedList<UserModel>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.User.AsNoTracking();
            if (request.SearchKey != null) {
                var searchWords = request.SearchKey.ToLower().Split(' ');
                query = query.Where(i => i.FullName.ToLower().Contains(searchWords[0])
                                  || i.UserName.ToLower().Contains(searchWords[0]));
                if (searchWords.Length > 1)
                {
                    for (int x = 1; x < searchWords.Length; x++)
                    {
                        var search = searchWords[x];
                        query = query.Where(i => i.FullName.ToLower().Contains(search)
                                  || i.UserName.ToLower().Contains(search));
                    }
                }            
            }
            switch (request.SortBy) {
                case "UserName":
                    if (request.OrderBy == "Asc") {
                        query = query.OrderBy(l=>l.UserName);
                    }
                    else {
                        query = query.OrderByDescending(l => l.UserName);
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
            }
            var pagedUser = new CustomPagedList<Data.Models.User>(query, request.PageIndex, request.PageSize); 
            var templateList = _mapper.Map<IList<Data.Models.User>, IList<UserModel>>(await pagedUser.Items.ToListAsync());
            return new CustomPagedList<UserModel>(templateList, pagedUser.PagedListMetaData);
        }
    }
}
