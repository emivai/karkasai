using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.AppointmentDTOs
{
    public class CreateAppointmentDTO
    {
        public Guid TimeSlotId { get; set; }

        public static Appointment ToDomain(CreateAppointmentDTO appointmentDTO, Guid petId, Guid userId)
        {
            return new Appointment
            {
                Status = AppointmentStatus.Scheduled,
                PetId = petId,
                TimeSlotId = appointmentDTO.TimeSlotId,
                UserId = userId
            };
        }
    }
}
