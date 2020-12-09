using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using webapi.Models;

namespace webapi
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "website",
                    ClientName = "Website client",

                    RedirectUris = { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:4200/", "44545" },
                    ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>() { "webapi"},
                    AllowAccessTokensViaBrowser = true,

                    AccessTokenLifetime = 3600,
                    AllowOfflineAccess = true
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("webapi", "The Backend")
            };
        }
    }
}
