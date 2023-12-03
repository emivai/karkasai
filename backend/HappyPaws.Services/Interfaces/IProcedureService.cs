using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface IProcedureService
    {
        public Task<Procedure> AddAsync(Procedure procedure);
        public Task<Procedure> GetAsync(Guid id);
        public Task<List<Procedure>> GetAllAsync();
        public Task<Procedure> UpdateAsync(Guid id, Procedure procedure);
        public Task DeleteAsync(Guid id);
    }
}
