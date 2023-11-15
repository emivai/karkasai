using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            base.Configure(builder);

            builder.Property(p => p.Type).HasColumnName("type").HasConversion<int>();

            builder.Property(p => p.Name).HasColumnName("name");

            builder.Property(p => p.Surname).HasColumnName("surname");

            builder.Property(p => p.Email).HasColumnName("email");

            builder.Property(p => p.PhoneNumber).HasColumnName("phone_number");

            builder.Property(p => p.Photo).HasColumnName("photo");

            builder.Property(p => p.Description).HasColumnName("description");
        }
    }
}
