using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class AppointmentProcedureRepository : IAppointmentProcedureRepository
    {
        private readonly DatabaseContext _context;

        public AppointmentProcedureRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<AppointmentProcedure> AddAsync(AppointmentProcedure appointmentProcedure)
        {
            _context.AppointmentProcedures.Add(appointmentProcedure);

            await _context.SaveChangesAsync();

            return appointmentProcedure;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = _context.AppointmentProcedures.FirstOrDefault(p => p.Id == id);

            if (fromDb is null) return;

            _context.AppointmentProcedures.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<AppointmentProcedure>> GetAllAsync(Guid appointmentId)
        {
            return await _context.AppointmentProcedures.Where(a => a.AppointmentId == appointmentId).ToListAsync();
        }

        public async Task<AppointmentProcedure> GetAsync(Guid id)
        {
            return await _context.AppointmentProcedures.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<AppointmentProcedure> UpdateAsync(Guid id, AppointmentProcedure appointmentProcedure)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.ProcedureId = appointmentProcedure.ProcedureId;
                fromDb.AppointmentId = appointmentProcedure.AppointmentId;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
