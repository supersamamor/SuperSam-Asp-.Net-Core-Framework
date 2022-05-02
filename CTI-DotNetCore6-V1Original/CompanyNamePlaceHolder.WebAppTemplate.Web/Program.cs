using AspNetCoreHero.ToastNotification.Extensions;
using CTI.Common.Web.Utility.Logging;
using CompanyNamePlaceHolder.WebAppTemplate.Web;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Identity;
using CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom
                          .Services(services).Enrich
                          .FromLogContext());

// Add services to the container.
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var identityStartup = new IdentityStartup(builder.Configuration);
identityStartup.ConfigureServices(builder.Services);

var app = builder.Build();

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
Log.Information("Seeding database");
var scope = app.Services.CreateScope();
await DefaultEntity.Seed(scope.ServiceProvider);
await DefaultRole.Seed(scope.ServiceProvider);
await DefaultUser.Seed(scope.ServiceProvider);
await DefaultClient.Seed(scope.ServiceProvider);
Log.Information("Finished seeding database");

app.Run();
