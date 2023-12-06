using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    internal class TimeSlotEntityTypeConfiguration : BaseEntityTypeConfiguration<TimeSlot>
    {
        public override void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.ToTable("time_slots");

            base.Configure(builder);

            builder.Property(p => p.Start).HasColumnName("beginning");

            builder.Property(p => p.End).HasColumnName("ending");

            builder.Property(p => p.Available).HasColumnName("available");

            builder.Property(p => p.UserId).HasColumnName("doctor_id");

            builder.HasOne(e => e.Doctor).WithMany(e => e.TimeSlots).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
