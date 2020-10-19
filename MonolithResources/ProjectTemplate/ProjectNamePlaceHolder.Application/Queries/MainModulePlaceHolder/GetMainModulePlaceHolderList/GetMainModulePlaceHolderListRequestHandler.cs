using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data;
using X.PagedList;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Models;

namespace ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderList
{
    public class GetMainModulePlaceHolderListRequestHandler : IRequestHandler<GetMainModulePlaceHolderListRequest, CustomPagedList<MainModulePlaceHolderModel>>
    {       
        
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public GetMainModulePlaceHolderListRequestHandler(ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<CustomPagedList<MainModulePlaceHolderModel>> Handle(GetMainModulePlaceHolderListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.MainModulePlaceHolder.AsNoTracking();
            if (request.SearchKey != null)
            {
                var searchWords = request.SearchKey.ToLower().Split(' ');
                query = query.Where(i => 
									 Template:[InsertNewSearchableZero]
								  );
                if (searchWords.Length > 1)
                {
                    for (int x = 1; x < searchWords.Length; x++)
                    {
                        var search = searchWords[x];
                        query = query.Where(i => 
									 Template:[InsertNewSearchableIndex]
								  );
                    }
                }            
            }
            switch (request.SortBy)
            {
				Template:[InsertNewListSorterAndOrder]            
            }         
            var pagedMainModulePlaceHolder = new CustomPagedList<Data.Models.MainModulePlaceHolder>(query, request.PageIndex, request.PageSize);
            var mainModulePlaceHolderList = _mapper.Map<IList<Data.Models.MainModulePlaceHolder>, IList<MainModulePlaceHolderModel>>(await pagedMainModulePlaceHolder.Items.ToListAsync());
            return new CustomPagedList<MainModulePlaceHolderModel>(mainModulePlaceHolderList, pagedMainModulePlaceHolder.PagedListMetaData);                     
        }
    }
}
