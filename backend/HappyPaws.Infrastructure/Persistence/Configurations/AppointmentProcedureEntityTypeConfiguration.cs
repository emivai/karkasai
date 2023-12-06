using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class AppointmentProcedureEntityTypeConfiguration : BaseEntityTypeConfiguration<AppointmentProcedure>
    {
        public override void Configure(EntityTypeBuilder<AppointmentProcedure> builder)
        {
            builder.ToTable("appointment_procedures");

            base.Configure(builder);

            builder.Property(p => p.ProcedureId).HasColumnName("procedure_id");

            builder.Property(p => p.AppointmentId).HasColumnName("appointment_id");

            builder.Property(p => p.UserId).HasColumnName("user_id");

            builder.HasOne(e => e.User).WithMany(e => e.AppointmentProcedures).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Procedure).WithMany().HasForeignKey(e => e.ProcedureId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Appointment).WithMany(e => e.AppointmentProcedures).HasForeignKey(e => e.AppointmentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
