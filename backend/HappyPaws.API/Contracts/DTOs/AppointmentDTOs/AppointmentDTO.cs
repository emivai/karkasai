using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.AppointmentDTOs
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public AppointmentStatus Status { get; set; }
        public Guid PetId { get; set; }
        public Guid TimeSlotId { get; set; }

        public Guid UserId { get; set; }

        public static AppointmentDTO FromDomain(Appointment appointment)
        {
            var appointmentDTO = new AppointmentDTO
            {
                Id = appointment.Id,
                Status = appointment.Status,
                PetId = appointment.PetId,
                TimeSlotId = appointment.TimeSlotId,
                Price = appointment.CalculateTotalPrice(),
                UserId = appointment.UserId
            };
   
            return appointmentDTO;
        }
    }
}
