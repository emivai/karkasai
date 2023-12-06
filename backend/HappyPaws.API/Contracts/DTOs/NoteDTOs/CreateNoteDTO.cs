using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.NoteDTOs
{
    public class CreateNoteDTO
    {
        public string Value { get; set; }

        public static Note ToDomain(CreateNoteDTO noteDTO, Guid appointmentId, Guid userId)
        {
            return new Note
            {
                Value = noteDTO.Value,
                AppointmentId = appointmentId,
                UserId = userId
            };
        }
    }
}
