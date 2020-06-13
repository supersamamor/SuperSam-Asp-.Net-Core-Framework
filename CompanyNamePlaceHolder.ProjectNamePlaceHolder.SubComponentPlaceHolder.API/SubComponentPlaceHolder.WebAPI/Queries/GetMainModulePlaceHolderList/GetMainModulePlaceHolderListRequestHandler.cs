using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SubComponentPlaceHolder.Data;
using SubComponentPlaceHolder.WebAPI.Models;
using X.PagedList;

namespace SubComponentPlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderList
{
    public class GetMainModulePlaceHolderListRequestHandler : IRequestHandler<GetMainModulePlaceHolderListRequest, CustomPagedList<MainModulePlaceHolderModel>>
    {       
        
        private readonly SubComponentPlaceHolderContext _context;
        private readonly IMapper _mapper;
        public GetMainModulePlaceHolderListRequestHandler(SubComponentPlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<CustomPagedList<MainModulePlaceHolderModel>> Handle(GetMainModulePlaceHolderListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.MainModulePlaceHolder.AsNoTracking();
            if (request.SearchKey != null) {
                var searchWords = request.SearchKey.ToLower().Split(' ');
                query = query.Where(i => i.Code.ToLower().Contains(searchWords[0])
                                  || i.Name.ToLower().Contains(searchWords[0]));
                if (searchWords.Length > 1)
                {
                    for (int x = 1; x < searchWords.Length; x++)
                    {
                        var search = searchWords[x];
                        query = query.Where(i => i.Code.ToLower().Contains(search)
                                  || i.Name.ToLower().Contains(search));
                    }
                }            
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
            var pagedMainModulePlaceHolder = new CustomPagedList<Data.Models.MainModulePlaceHolder>(query, request.PageIndex, request.PageSize); 
            var mainModulePlaceHolderList = _mapper.Map<IList<Data.Models.MainModulePlaceHolder>, IList<MainModulePlaceHolderModel>>(await pagedMainModulePlaceHolder.Items.ToListAsync());
            return new CustomPagedList<MainModulePlaceHolderModel>(mainModulePlaceHolderList, pagedMainModulePlaceHolder.PagedListMetaData);
        }
    }
}
