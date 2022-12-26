using domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.persistence
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired(false);
            builder.Property(x => x.FamilyName).IsRequired(false); 
            builder.Property(x => x.UserName).IsRequired(false);
            builder.Property(x => x.Password).IsRequired(false);
        }
    }
}
