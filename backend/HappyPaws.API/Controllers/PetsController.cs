using HappyPaws.API.Auth.Policies;
using HappyPaws.API.Contracts.DTOs.PetDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Enums;
using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthorizationService _authorizationService;

        public PetsController(IPetService petsService, IUserService usersService, IAuthorizationService authorizationService)
        {
            _petsService = petsService;
            _usersService = usersService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<PetDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            List<Core.Entities.Pet> pets;

            if (User.IsInRole("Client")) pets = await _petsService.GetAllAsync(new Guid(User.FindFirst("UserId")?.Value));
            else if (User.IsInRole("Doctor")) pets = await _petsService.GetAllForDoctorAsync(new Guid(User.FindFirst("UserId")?.Value));
            else pets = await _petsService.GetAllAsync(null);

            pets = await _petsService.GetAllAsync(null);
            var result = pets.Select(PetDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var pet = await _petsService.GetAsync(id) ?? throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, pet, PolicyNames.Owner);

            if (User.IsInRole("Client") && !authResult.Succeeded)
            {
                return Forbid();
            }

            return Ok(PetDTO.FromDomain(pet));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreatePetDTO petDTO)
        {
            var userId = new Guid(User.FindFirst("UserId").Value);

            var owner = await _usersService.GetAsync(userId);

            if (owner == null) throw new ResourceNotFoundException();

            var created = await _petsService.AddAsync(CreatePetDTO.ToDomain(petDTO, userId));

            return StatusCode(StatusCodes.Status201Created, PetDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatePetDTO petDTO)
        {
            var pet = await _petsService.GetAsync(id);

            if (pet == null) throw new ResourceNotFoundException();

            var authResult = await _authorizationService.AuthorizeAsync(User, pet, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var updated = await _petsService.UpdateAsync(id, UpdatePetDTO.ToDomain(petDTO));

            return Ok(PetDTO.FromDomain(updated));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var pet = await _petsService.GetAsync(id) ?? throw new ResourceNotFoundException();

            if (pet.Appointments != null && pet.Appointments.Any(a => a.Status == AppointmentStatus.Scheduled)) throw new BadRequestException("Pet that has appointments with status 'scheduled' cannot be deleted.");

            var authResult = await _authorizationService.AuthorizeAsync(User, pet, PolicyNames.Owner);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _petsService.DeleteAsync(id);

            return NoContent();
        }
    }
}
