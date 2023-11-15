using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class NoteEntityTypeConfiguration : BaseEntityTypeConfiguration<Note>
    {
        public override void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("notes");

            base.Configure(builder);

            builder.Property(p => p.Value).HasColumnName("value");

            builder.Property(p => p.AppointmentId).HasColumnName("appointment_id");

            builder.HasOne(e => e.Appointment).WithMany(e => e.Notes).HasForeignKey(e => e.AppointmentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
