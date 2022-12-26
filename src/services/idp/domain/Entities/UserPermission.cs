namespace domain.Entities
{
    public class UserPermission
    {
        public int Id { get; private set; }
        public int PermissionId { get; private set; }
        public Permission Permission { get; private set; }

        private UserPermission()
        {
        }

        private UserPermission(int permissionId) : this()
        {
            PermissionId = permissionId;
        }

        public static UserPermission AddUserPermission(int permissionId)
        {
            return new UserPermission(permissionId);
        }
    }
}
