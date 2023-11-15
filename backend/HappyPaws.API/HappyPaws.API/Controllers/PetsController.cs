using HappyPaws.API.Contracts.DTOs.PetDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Enums;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petsService;
        private readonly IUserService _usersService;

        public PetsController(IPetService petsService, IUserService usersService)
        {
            _petsService = petsService;
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PetDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            var pets = await _petsService.GetAllAsync();

            var result = pets.Select(PetDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var pet = await _petsService.GetAsync(id) ?? throw new ResourceNotFoundException();

            return Ok(PetDTO.FromDomain(pet));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreatePetDTO petDTO)
        {
            if (!ModelState.IsValid)
                throw new ResourceNotFoundException(); ;

            var owner = await _usersService.GetAsync(petDTO.OwnerId);

            if (owner == null) throw new ResourceNotFoundException();

            if (owner.Type != UserType.Client) throw new UserTypeException(UserType.Client);

            var created = await _petsService.AddAsync(CreatePetDTO.ToDomain(petDTO));

            return StatusCode(StatusCodes.Status201Created, PetDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatePetDTO petDTO)
        {
            var pet = await _petsService.GetAsync(id);

            if (pet == null) throw new ResourceNotFoundException();

            var updated = await _petsService.UpdateAsync(id, UpdatePetDTO.ToDomain(petDTO));

            return Ok(PetDTO.FromDomain(updated));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var pet = await _petsService.GetAsync(id) ?? throw new ResourceNotFoundException();

            if (pet.Appointments != null && pet.Appointments.Any(a => a.Status == AppointmentStatus.Scheduled)) throw new BadRequestException("Pet that has appointments with status 'scheduled' cannot be deleted.");

            await _petsService.DeleteAsync(id);

            return NoContent();
        }
    }
}
