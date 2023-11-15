using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.TimeSlotDTOs
{
    public class CreateTimeSlotDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Available { get;} = true;
        public Guid DoctorId { get; set; }

        public static TimeSlot ToDomain(CreateTimeSlotDTO timeSlotDTO)
        {
            return new TimeSlot
            {
                Start = timeSlotDTO.Start,
                End = timeSlotDTO.End,
                Available = timeSlotDTO.Available,
                DoctorId = timeSlotDTO.DoctorId
            };
        }
    }
}
