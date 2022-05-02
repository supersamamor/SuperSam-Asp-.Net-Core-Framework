using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.WebAppTemplate.API.Controllers;

public class MetaDataController : BaseApiController<MetaDataController>
{
    readonly IConfiguration Configuration;

    public MetaDataController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpGet]
    public ActionResult<MetaData> GetAsync()
    {
        var version = new Version();
        Configuration.GetSection("Version").Bind(version);
        return Ok(new MetaData { Version = version });
    }
}

public record MetaData
{
    public Version Version { get; init; } = new();
}

public record Version
{
    public string ReleaseName { get; init; } = "";
    public string BuildNumber { get; init; } = "";
}
