using HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs;
using HappyPaws.API.Contracts.DTOs.NoteDTOs;
using HappyPaws.API.Contracts.DTOs.PetDTOs;
using HappyPaws.API.Contracts.DTOs.TimeSlotDTOs;
using HappyPaws.API.Contracts.DTOs.UserDTOs;
using HappyPaws.Core.Entities;
using HappyPaws.Core.Enums;

namespace HappyPaws.API.Contracts.DTOs.AppointmentDTOs
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public AppointmentStatus Status { get; set; }
        public PetDTO? Pet { get; set; }
        public TimeSlotDTO? TimeSlot { get; set; }
        public List<NoteDTO>? Notes { get; set; }
        public List<AppointmentProcedureDTO>? AppointmentProcedures { get; set; }
        public UserDTO? User { get; set; }

        public static AppointmentDTO FromDomain(Appointment appointment)
        {
            var appointmentDTO = new AppointmentDTO
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Pet = PetDTO.FromDomain(appointment.Pet),
                TimeSlot = TimeSlotDTO.FromDomain(appointment.TimeSlot),
                Price = appointment.CalculateTotalPrice(),
                User = UserDTO.FromDomain(appointment.User),
                Notes = appointment.Notes?.Select(NoteDTO.FromDomain).ToList(),
                AppointmentProcedures = appointment.AppointmentProcedures.Select(AppointmentProcedureDTO.FromDomain).ToList(),
            };
   
            return appointmentDTO;
        }
    }
}
