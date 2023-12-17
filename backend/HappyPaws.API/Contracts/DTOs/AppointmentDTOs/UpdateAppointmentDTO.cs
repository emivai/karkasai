using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.AppointmentDTOs
{
    public class UpdateAppointmentDTO
    {
        public AppointmentStatus Status { get; set; }
        //public Guid TimeSlotId { get; set; }

        public static Appointment ToDomain(UpdateAppointmentDTO appointmentDTO, Guid petId, Guid userId)
        {
            return new Appointment
            {
                Status = appointmentDTO.Status,
                PetId = petId,
                //TimeSlotId = appointmentDTO.TimeSlotId,
                UserId = userId
            };
        }
    }
}
