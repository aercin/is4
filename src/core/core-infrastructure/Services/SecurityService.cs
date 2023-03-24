using core_application.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace core_infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _config;
        public SecurityService(IConfiguration config)
        {
            this._config = config;
        }

        public string HashPassword(string plainPass)
        {
            byte[] salt = Encoding.UTF8.GetBytes(this._config.GetValue<string>("Security:Salt"));

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                                           Encoding.UTF8.GetBytes(plainPass),
                                           salt,
                                           this._config.GetValue<int>("Security:Iterations"),
                                           HashAlgorithmName.SHA512,
                                           this._config.GetValue<int>("Security:KeySize"));

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string plainPass, string hashedPass)
        {
            byte[] salt = Encoding.UTF8.GetBytes(this._config.GetValue<string>("Security:Salt"));

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                                                    plainPass,
                                                    salt,
                                                    this._config.GetValue<int>("Security:Iterations"),
                                                    HashAlgorithmName.SHA512,
                                                    this._config.GetValue<int>("Security:KeySize"));

            return hashToCompare.SequenceEqual(Convert.FromHexString(hashedPass));
        }
    }
}
