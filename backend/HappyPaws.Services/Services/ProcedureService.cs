using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Interfaces;

namespace HappyPaws.Application.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureService(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        public async Task<Procedure> AddAsync(Procedure procedure)
        {
            return await _procedureRepository.AddAsync(procedure);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _procedureRepository.DeleteAsync(id);
        }

        public async Task<List<Procedure>> GetAllAsync()
        {
            return await _procedureRepository.GetAllAsync();
        }

        public async Task<Procedure> GetAsync(Guid id)
        {
            return await _procedureRepository.GetAsync(id);
        }

        public async Task<Procedure> UpdateAsync(Guid id, Procedure procedure)
        {
            return await _procedureRepository.UpdateAsync(id, procedure);
        }
    }
}
