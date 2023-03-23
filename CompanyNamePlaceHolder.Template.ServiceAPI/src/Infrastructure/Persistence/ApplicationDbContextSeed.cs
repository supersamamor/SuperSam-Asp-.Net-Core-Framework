using Microsoft.EntityFrameworkCore;
using System;
//using ProjectNamePlaceHolder.Services.Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {        
        
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            await Task.Yield();
        }
    }
}
