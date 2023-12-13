using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DatabaseContext _context;

        public AppointmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = _context.Appointments.Include(a => a.Notes).FirstOrDefault(p => p.Id == id);

            if (fromDb is null) return;

            _context.Appointments.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAllAsync(Guid petId)
        {
            return await _context.Appointments
                .Include(a => a.TimeSlot).ThenInclude(t => t.Doctor)
                .Include(a => a.AppointmentProcedures).ThenInclude(p => p.Procedure)
                .Include(a => a.Notes)
                .Include(a => a.User)
                .Include(a => a.Pet)
                .Where(a => a.PetId == petId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAllForDoctorAsync(Guid petId, Guid doctorId)
        {
            return await _context.Appointments
                .Include(a => a.TimeSlot).ThenInclude(t => t.Doctor)
                .Include(a => a.AppointmentProcedures).ThenInclude(p => p.Procedure)
                .Include(a => a.Notes)
                .Include(a => a.User)
                .Include(a => a.Pet)
                .Where(a => a.PetId == petId && a.TimeSlot.UserId == doctorId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAllOutdatedAsync()
        {
            return await _context.Appointments.Include(a => a.TimeSlot).Where(a => a.TimeSlot.End < DateTime.UtcNow).ToListAsync();
        }

        public async Task<Appointment> GetAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.TimeSlot)
                .Include(a => a.AppointmentProcedures).ThenInclude(p => p.Procedure)
                .Include(a => a.Notes)
                .Include(a => a.User)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Appointment> UpdateAsync(Guid id, Appointment appointment)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.Status = appointment.Status;
                fromDb.PetId = appointment.PetId;
                fromDb.TimeSlotId = appointment.TimeSlotId;
                fromDb.UserId = appointment.UserId;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
