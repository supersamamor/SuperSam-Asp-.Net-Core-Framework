using CTI.Common.API;
using CTI.Common.Web.Utility.Logging;
using CTI.LocationApi.Application;
using CTI.LocationApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom
                          .Services(services).Enrich
                          .FromLogContext());

// Add services to the container.

var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddDefaultApiServices(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationContext>();
if (configuration.GetValue<bool>("UseInMemoryDatabase"))
{
    builder.Services.AddDbContext<ApplicationContext>(options
        => options.UseInMemoryDatabase("ApplicationContext"));
}
else
{
    builder.Services.AddDbContext<ApplicationContext>(options
        => options.UseSqlServer(configuration.GetConnectionString("ApplicationContext")));
}
builder.Services.AddDbContext<IdentityContext>(options
       => options.UseSqlServer(configuration.GetConnectionString("ApplicationContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.EnableSwagger();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.EnrichDiagnosticContext();
app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
