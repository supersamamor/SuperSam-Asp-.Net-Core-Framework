using AspNet.Security.OpenIdConnect.Primitives;
using Cti.Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string userId)
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

            return Task.FromResult(_username);
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }
    }
}
