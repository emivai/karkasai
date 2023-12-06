using HappyPaws.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HappyPaws.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentProcedure> AppointmentProcedures => Set<AppointmentProcedure>();
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<Procedure> Procedures => Set<Procedure>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
