using AspNetCoreHero.ToastNotification.Extensions;
using CTI.Common.Web.Utility.Logging;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using CTI.SQLReportAutoSender.Web;
using CTI.SQLReportAutoSender.Web.Areas.Identity;
using CTI.SQLReportAutoSender.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.FileProviders;
using Serilog;
using CTI.SQLReportAutoSender.Scheduler;
using CTI.SQLReportAutoSender.EmailSending;

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
services.AddScheduler(configuration);
services.AddEmailSendingAService(configuration);
var app = builder.Build();
// Static Files
var uploadFilesPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
bool uploadFilesPathExists = System.IO.Directory.Exists(uploadFilesPath);
if (!uploadFilesPathExists)
    System.IO.Directory.CreateDirectory(uploadFilesPath);
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 365;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    },
    FileProvider = new PhysicalFileProvider(uploadFilesPath),
    RequestPath = "/" + WebConstants.UploadFilesPath
});
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
                builder.AddDefaultSrc().OverHttps();
                builder.AddScriptSrc()
                       .Self()
                       .StrictDynamic()
                       .WithNonce()
                       .UnsafeInline()
                       .UnsafeEval()
                       .OverHttps();
                builder.AddStyleSrc()
                       .Self()
                       .UnsafeInline()
                       .OverHttps();
                builder.AddImgSrc().OverHttps().Data();
                builder.AddObjectSrc().None();
                builder.AddBaseUri().None();
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
if (configuration.GetValue<bool>("IsIdentityServerEnabled"))
{
    Log.Information("Seeding database");
    var scope = app.Services.CreateScope();
    await DefaultEntity.Seed(scope.ServiceProvider);
    await DefaultRole.Seed(scope.ServiceProvider);
    await DefaultUser.Seed(scope.ServiceProvider);
    await DefaultClient.Seed(scope.ServiceProvider);
    await DefaultApprovalSetup.Seed(scope.ServiceProvider);
    Log.Information("Finished seeding database");
}
app.Run();
