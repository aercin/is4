using core_domain.Abstractions;

namespace domain.Entities
{
    public class Permission : IAggregateRoot
    {
        public int Id { get; private set; } 
        public string Scope { get; private set; }
        public string Description { get; private set; }
        private Permission()
        {
        }

        private Permission(string scope, string description) : this()
        {
            Scope = scope;
            Description = description;
        }

        public static Permission AddPermission(string scope, string desc)
        {
            return new Permission(scope, desc);
        }
    }
}
