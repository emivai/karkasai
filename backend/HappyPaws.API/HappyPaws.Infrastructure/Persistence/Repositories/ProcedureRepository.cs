using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {
        private readonly DatabaseContext _context;

        public ProcedureRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Procedure> AddAsync(Procedure procedure)
        {
            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();

            return procedure;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = _context.Procedures.FirstOrDefault(p => p.Id == id);

            if (fromDb is null) return;

            _context.Procedures.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Procedure>> GetAllAsync()
        {
            return await _context.Procedures.ToListAsync();
        }

        public async Task<Procedure> GetAsync(Guid id)
        {
            return await _context.Procedures.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Procedure> UpdateAsync(Guid id, Procedure procedure)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.Price = procedure.Price;
                fromDb.Description = procedure.Description;
                fromDb.Name = procedure.Name;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
