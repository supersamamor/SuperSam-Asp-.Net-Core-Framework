using AspNetCoreHero.ToastNotification.Extensions;
using CompanyPL.Common.Web.Utility.Logging;
using CompanyPL.ProjectPL.Infrastructure.Data;
using CompanyPL.ProjectPL.Web;
using CompanyPL.ProjectPL.Web.Areas.Identity;
using CompanyPL.ProjectPL.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using CompanyPL.ProjectPL.EmailSending;
using CompanyPL.ProjectPL.ExcelProcessor;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom
                          .Services(services).Enrich
                          .FromLogContext());

// Add services to the container.
var configuration = builder.Configuration;
var services = builder.Services;

services.ConfigureIdentityServices(configuration);
services.ConfigureDefaultServices(configuration);

if (configuration.GetValue<bool>("UseInMemoryDatabase"))
{
    services.AddDbContext<ApplicationContext>(options =>
        options.UseInMemoryDatabase("ApplicationContext"));
}
else
{
    services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ApplicationContext"),
                             o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
}
services.AddHealthChecks()
        .AddDbContextCheck<ApplicationContext>()
        .AddDbContextCheck<IdentityContext>();
services.AddEmailSendingAService(configuration);
services.AddExcelProcessor();

var app = builder.Build();
// Static Files
var uploadFilesPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
if (uploadFilesPath != null)
{
    bool uploadFilesPathExists = System.IO.Directory.Exists(uploadFilesPath);
    if (!uploadFilesPathExists)
        System.IO.Directory.CreateDirectory(uploadFilesPath);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSecurityHeaders(policies =>
    policies.AddDefaultSecurityHeaders()
            .AddContentSecurityPolicy(builder =>
            {
                builder.AddUpgradeInsecureRequests();
                builder.AddBlockAllMixedContent();
                builder.AddDefaultSrc().Self().OverHttps();
                builder.AddScriptSrc()
                       .Self()
                       .StrictDynamic()
                       .WithNonce()
                       .OverHttps();
                builder.AddStyleSrc()
                       .Self()                      
                       .OverHttps();
                builder.AddImgSrc().OverHttps().Data();
                builder.AddObjectSrc().None();
                builder.AddBaseUri().None();
                builder.AddFrameAncestors().Self();
                builder.AddFormAction().OverHttps();
            }));
app.UseWebOptimizer();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 365;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});
app.UseCookiePolicy();
app.UseSerilogRequestLogging();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.EnrichDiagnosticContext();
app.MapControllers();
app.MapRazorPages();
app.MapHealthChecks("/health").AllowAnonymous();
app.UseNotyf();

// Seed the database
Log.Information("Seeding database");
var scope = app.Services.CreateScope();
await DefaultEntity.Seed(scope.ServiceProvider);
await DefaultRole.Seed(scope.ServiceProvider);
await DefaultUser.Seed(scope.ServiceProvider);
await DefaultClient.Seed(scope.ServiceProvider);
await UserRole.Seed(scope.ServiceProvider);
await DefaultDashboard.Seed(scope.ServiceProvider);
Log.Information("Finished seeding database");
app.Run();
