using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyPaws.Infrastructure.Persistence.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DatabaseContext _context;

        public PetRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Pet> AddAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return pet;
        }

        public async Task DeleteAsync(Guid id)
        {
            var fromDb = await _context.Pets.Include(p => p.Appointments).ThenInclude(a => a.Notes).FirstOrDefaultAsync(p => p.Id == id);

            if (fromDb is null) return;

            _context.Pets.Remove(fromDb);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Pet>> GetAllAsync(Guid? userId = null)
        {
            if(userId != null)
                return await _context.Pets.Where(pet => pet.UserId == userId).Include(p => p.Appointments).ToListAsync();

            return await _context.Pets.Include(p => p.Appointments).ToListAsync();
        }

        public async Task<List<Pet>> GetAllForDoctorAsync(Guid userId)
        {
            var petIds = await _context.Appointments.Include(a => a.TimeSlot).Where(a => a.TimeSlot.UserId == userId).Select(a => a.PetId).ToListAsync();

            return await _context.Pets.Include(p => p.Appointments).Where(p => petIds.Contains(p.Id)).ToListAsync();
        }

        public async Task<Pet> GetAsync(Guid id)
        {
            return await _context.Pets.Include(p => p.Appointments).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pet> UpdateAsync(Guid id, Pet pet)
        {
            var fromDb = await GetAsync(id);

            if (fromDb != null)
            {
                fromDb.Type = pet.Type;
                fromDb.Name = pet.Name;
                fromDb.Birthdate = pet.Birthdate;
                fromDb.Photo = pet.Photo ?? fromDb.Photo;
            }

            await _context.SaveChangesAsync();

            return fromDb;
        }
    }
}
