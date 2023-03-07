using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Security.Cryptography;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(legacy_api.Startup))]
namespace legacy_api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config); // bootstrap your existing WebApi config 

            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //Modulus ve Exponent bilgileri is provider discovery dokümanında yer alan jwks adresi üzerinden alındı.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(
              new RSAParameters()
              {
                  Modulus = FromBase64Url("vIaPuDUl3l6a60x3NIL02QXzC0oFVD_YgUvnE7FJUH4FirjuFvRNXR3MCYN1LLq6pdIdZ7yiAeuB5f1j0HeCaf3wo1Giw_AUQsH6GhZhN7GtpS4bHd-uhWq43vwDE7xPy1UDWfhAf1xsjyPJjV-sT3iywz2vBoGwdapu0QJnb66k36cHUx71LEq0xsIbz9LglX21C5YyF0of9tZ7XWtEVgyIjRtnxmD5lR1ylGEQf2jBQJA1G6qkqHmoxZ2XXj1FaEDxznlNkbNjDg4fOjZ4t06WlO6rzugirV_Po6UMfcfdC3B4Sbv48QzM1LcdlqFNECZahFusa7j_RRFk1KdPyQ"),
                  Exponent = FromBase64Url("AQAB")
              });

            appBuilder.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,//if it finds a token is valid, it will set User.Identity accordingly. 
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = "legacy-resource-api",//identity providerda bu apiyi temsil eden resource name
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:5080",//identity provider base url
                    ValidateIssuerSigningKey = true, // I guess you don't even have to sign the token 
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                }
            });
            appBuilder.UseWebApi(config); // instruct OWIN to take over                       
        }

        private byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
    }
}