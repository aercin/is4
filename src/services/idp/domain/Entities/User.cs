using core_domain.Abstractions;

namespace domain.Entities
{
    public class User : IAggregateRoot
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string FamilyName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        public List<UserPermission> Permissions { get; private set; }

        private User()
        {
            Permissions = new List<UserPermission>();
            IsActive = false;
        }

        private User(string eMail) : this()
        {
            Email = eMail;
        }

        public static User AddUser(string eMail)
        {
            return new User(eMail);
        }

        public void ModifyUser(string firstName, string familyName, string userName, string password)
        {
            this.FirstName = firstName;
            this.FamilyName = familyName;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = true;
        }
         
        public void AddUserPermission(int permissionId)
        {
            Permissions.Add(UserPermission.AddUserPermission(permissionId));
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
