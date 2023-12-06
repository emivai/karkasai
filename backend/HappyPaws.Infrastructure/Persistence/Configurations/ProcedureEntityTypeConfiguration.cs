using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class ProcedureEntityTypeConfiguration : BaseEntityTypeConfiguration<Procedure>
    {
        public override void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.ToTable("procedures");

            base.Configure(builder);

            builder.Property(p => p.Name).HasColumnName("name");

            builder.Property(p => p.Description).HasColumnName("description");

            builder.Property(p => p.Price).HasColumnName("price");
        }
    }
}
