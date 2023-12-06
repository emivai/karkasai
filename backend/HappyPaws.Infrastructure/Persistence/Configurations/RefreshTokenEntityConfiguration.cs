using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenEntityConfiguration : BaseEntityTypeConfiguration<RefreshTokenEntity>
    {
        public override void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("refresh_tokens");

            base.Configure(builder);

            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Token).HasColumnName("token");
            builder.Property(p => p.Expires).HasColumnName("expires");
        }           
    }
}
