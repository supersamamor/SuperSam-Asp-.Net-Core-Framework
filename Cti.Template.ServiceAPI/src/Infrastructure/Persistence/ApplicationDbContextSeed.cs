using Microsoft.EntityFrameworkCore;
using System;
//using CHANGE_TO_APP_NAME.Services.Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {        
        
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            await Task.Yield();
        }
    }
}
