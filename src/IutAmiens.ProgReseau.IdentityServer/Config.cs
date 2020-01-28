using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IutAmiens.ProgReseau.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiResource> Apis => new List<ApiResource>
        {
            new ApiResource("monApi", "Mon API"),
            new ApiResource("autreScope", "Autre scope")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "autreScope",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "autreScope" }
            },

            new Client
            {
                ClientId = "console",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "monApi" }
            },

            new Client
            {
                ClientId = "webUi",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                RequirePkce = true,

                RedirectUris = { "http://localhost:5002/signin-oidc" },

                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    //"monApi"
                },

                //AllowOfflineAccess = true
            }
        };
    }
}