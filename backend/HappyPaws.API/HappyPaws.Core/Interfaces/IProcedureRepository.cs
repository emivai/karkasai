using HappyPaws.Core.Entities;

namespace HappyPaws.Core.Interfaces
{
    public interface IProcedureRepository
    {
        public Task<Procedure> AddAsync(Procedure procedure);
        public Task<Procedure> GetAsync(Guid id);
        public Task<List<Procedure>> GetAllAsync();
        public Task<Procedure> UpdateAsync(Guid id, Procedure procedure);
        public Task DeleteAsync(Guid id);
    }
}
