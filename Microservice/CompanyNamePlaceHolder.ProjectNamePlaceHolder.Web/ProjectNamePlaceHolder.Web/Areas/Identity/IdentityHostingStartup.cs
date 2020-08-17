using Microsoft.AspNetCore.Hosting;
[assembly: HostingStartup(typeof(ProjectNamePlaceHolder.Web.Areas.Identity.IdentityHostingStartup))]
namespace ProjectNamePlaceHolder.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}