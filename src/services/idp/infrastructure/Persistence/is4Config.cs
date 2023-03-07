using IdentityServer4.Models;

namespace infrastructure.Persistence
{
    internal class is4Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("secure-api-for-only-auth","Grant for accessing Resource Api"),
                new ApiScope("secure-api-forecast-for-five-days","Grant for accessing forecast for five days ep of Resource Api"),
                new ApiScope("secure-api-forecast-for-ten-days","Grant for accessing forecast for ten days ep of Resource Api"),
                new ApiScope("secure-api-instant-forecast","Grant for accessing instant forecast ep of Resource Api"),
                new ApiScope("secure-api-full-access","Grant for accessing any ep of Resource Api"),
                new ApiScope("identityServerApi", "Grant for accessing Identity Server Api"),
                new ApiScope("legacy-resource-api-full-access","Grant for accessing Legacy Resource Api")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name="resource-api",
                    Scopes = new List<string>{ "secure-api-for-only-auth",
                                               "secure-api-forecast-for-five-days",
                                               "secure-api-forecast-for-ten-days",
                                               "secure-api-instant-forecast",
                                               "secure-api-full-access"}
                },
                new ApiResource
                {
                    Name="identity-server-api",
                    Scopes=new List<string>{ "identityServerApi" }
                },
                new ApiResource
                {
                    Name="legacy-resource-api",
                    Scopes=new List<string>{ "legacy-resource-api-full-access"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="m2m-forecast-5-days",
                    ClientName = "Sadece 5 günlük hava tahmini yapabilecek M2M Client",
                    ClientSecrets = new List<Secret> { new Secret("m2m5days".Sha256()) },
                    AllowedGrantTypes =  GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string> { "secure-api-forecast-for-five-days" }
                },
                new Client
                {
                    ClientId="m2m-forecast-both-5-and-10-days",
                    ClientName = "Hem 5 günlük hem de 10 günlük hava tahmini yapabilecek M2M Client",
                    ClientSecrets = new List<Secret> { new Secret("m2m10days".Sha256()) },
                    AllowedGrantTypes =  GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string> { "secure-api-forecast-for-five-days","secure-api-forecast-for-ten-days" }
                },
                new Client
                {
                    ClientId="m2m-instant-forecast",
                    ClientName = "Sadece anlık hava tahmini yapabilecek M2M Client",
                    ClientSecrets = new List<Secret> { new Secret("m2mNow".Sha256()) },
                    AllowedGrantTypes =  GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string> { "secure-api-instant-forecast" }
                },
                new Client
                {
                    ClientId="m2m-forecast-without-restriction",
                    ClientName = "Herhangi bir kısıtlama olmaksızın hava tahmini yapabilecek M2M Client",
                    ClientSecrets = new List<Secret> { new Secret("m2mFullAccess".Sha256()) },
                    AllowedGrantTypes =  GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string> { "secure-api-full-access" }
                },
                new Client
                {
                    ClientId="interactive-client",
                    ClientName = "Interactive Console Client",
                    ClientSecrets = new List<Secret> { new Secret("interactive".Sha256()) },
                    AllowedGrantTypes =  GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new List<string> { "identityServerApi", "openid", "secure-api-for-only-auth", "legacy-resource-api-full-access" },
                    AccessTokenLifetime = 3600,//1sa
                    AllowOfflineAccess = true,//refresh token akışı bu client için kullanılacak.
                    RefreshTokenUsage = TokenUsage.ReUse // refresh token, her access token talebinde değişmesin.
                    //Refresh token expiration için default ayarlar ile ilerliyoruz. Absolute expiration yani. varsayılan değeri 30 gün   
                }
            };
        }
    }
}
