using HappyPaws.API.Contracts.DTOs.ProcedureDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProceduresController : ControllerBase
    {
        private readonly IProcedureService _procedureService;

        public ProceduresController(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ProcedureDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            var procedures = await _procedureService.GetAllAsync();

            var result = procedures.Select(ProcedureDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProcedureDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var procedure = await _procedureService.GetAsync(id);

            if (procedure == null) throw new ResourceNotFoundException();

            return Ok(ProcedureDTO.FromDomain(procedure));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProcedureDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateProcedureDTO noteDTO)
        {
            var created = await _procedureService.AddAsync(CreateProcedureDTO.ToDomain(noteDTO));

            return StatusCode(StatusCodes.Status201Created, ProcedureDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProcedureDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateProcedureDTO noteDTO)
        {
            var procedure = _procedureService.GetAsync(id);

            if (procedure == null) throw new ResourceNotFoundException();

            var updated = await _procedureService.UpdateAsync(id, UpdateProcedureDTO.ToDomain(noteDTO));

            return Ok(ProcedureDTO.FromDomain(updated));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var procedure = _procedureService.GetAsync(id);

            if (procedure == null) throw new ResourceNotFoundException();

            await _procedureService.DeleteAsync(id);

            return NoContent();
        }
    }
}
