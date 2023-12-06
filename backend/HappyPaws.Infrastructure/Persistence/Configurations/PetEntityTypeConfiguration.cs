using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class PetEntityTypeConfiguration : BaseEntityTypeConfiguration<Pet>
    {
        public override void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            base.Configure(builder);

            builder.Property(p => p.Type).HasColumnName("type").HasConversion<int>();

            builder.Property(p => p.Name).HasColumnName("name");

            builder.Property(p => p.Birthdate).HasColumnName("birth_date");

            builder.Property(p => p.Photo).HasColumnName("photo");

            builder.Property(p => p.UserId).HasColumnName("owner_id");

            builder.HasOne(e => e.Owner).WithMany(e => e.Pets).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
