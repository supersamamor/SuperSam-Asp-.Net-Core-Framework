using AspNet.Security.OpenIdConnect.Primitives;
using Cti.Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ProjectNamePlaceHolder.Services.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var _userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Subject);

                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = Username;
                }

                return _userId;
            }
        }

        public string Username
        {
            get
            {
                var _username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.PreferredUsername);

                if (string.IsNullOrEmpty(_username))
                    _username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Username);

                if (string.IsNullOrEmpty(_username))
                {
                    var _clientId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.ClientId);
                    if (!string.IsNullOrEmpty(_clientId))
                    {
                        var _clientUser = _httpContextAccessor.HttpContext?.Request.Headers["X-Authenticated-Client-User"];
                        if (!string.IsNullOrEmpty(_clientUser))
                            _username = _clientUser;
                        else
                            _username = _clientId;
                    }
                }

                return _username;
            }
        }
    }
}
