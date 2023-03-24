namespace core_application.Abstractions
{
    public interface ISecurityService
    {
        string HashPassword(string plainPass);

        bool VerifyPassword(string plainPass, string hashedPass);
    }
}
