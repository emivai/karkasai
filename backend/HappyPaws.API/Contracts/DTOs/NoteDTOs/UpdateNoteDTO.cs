using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.NoteDTOs
{
    public class UpdateNoteDTO
    {
        public string Value { get; set; }
        public Guid AppointmentId { get; set; }

        public static Note ToDomain(UpdateNoteDTO noteDTO, Guid userId)
        {
            return new Note
            {
                Value = noteDTO.Value,
                AppointmentId = noteDTO.AppointmentId,
                UserId = userId
            };
        }
    }
}
