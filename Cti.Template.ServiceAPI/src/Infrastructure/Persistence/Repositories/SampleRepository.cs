using Cti.Core.Application.Common.Interfaces;
using Cti.Core.Infrastructure.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Persistence.Repositories
{
    internal class TestRepository : GenericRepositoryAsync<SampleEntity>
    {
        public TestRepository(ApplicationDbContext dbContext, IDateTime dateTimeService, ICurrentUserService currentUserService) : base(dbContext, dateTimeService, currentUserService)
        {            
        }
    }

    internal class SampleEntity
    {
    }
}
