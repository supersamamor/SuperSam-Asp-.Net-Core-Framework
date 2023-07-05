using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CNPlaceHolder.Common.API.Controllers;

/// <summary>
/// An API controller for displaying API info such as version, etc.
/// </summary>
public class MetaDataController : BaseApiController<MetaDataController>
{
    readonly IConfiguration Configuration;

    /// <summary>
    /// Creates a new instance of <see cref="MetaDataController"/>
    /// </summary>
    /// <param name="configuration">Instance of <see cref="IConfiguration"/></param>
    public MetaDataController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// The GET handler.
    /// </summary>
    /// <returns><see cref="ActionResult"/>&lt;<see cref="MetaData"/>&gt;</returns>
    [HttpGet]
    public ActionResult<MetaData> GetAsync()
    {
        var version = new Version();
        Configuration.GetSection("Version").Bind(version);
        return Ok(new MetaData { Version = version });
    }
}

/// <summary>
/// A class representing the API metadata.
/// </summary>
public record MetaData
{
    /// <summary>
    /// The version info of the API.
    /// </summary>
    public Version Version { get; init; } = new();
}

/// <summary>
/// A class representing the version of the API.
/// </summary>
public record Version
{
    /// <summary>
    /// The release name associated with this version of the API.
    /// </summary>
    public string ReleaseName { get; init; } = "";
    /// <summary>
    /// The build number associated with this version of the API.
    /// </summary>
    public string BuildNumber { get; init; } = "";
}
