using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.NoteDTOs
{
    public class NoteDTO
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid AppointmentId { get; set; }
        public User User { get; set; }

        public static NoteDTO FromDomain(Note note)
        {
            return new NoteDTO
            {
                Id = note.Id,
                Value = note.Value,
                AppointmentId = note.AppointmentId,
                User = note.User
            };
        }
    }
}
