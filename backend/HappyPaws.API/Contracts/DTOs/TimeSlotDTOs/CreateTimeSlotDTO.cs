using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.TimeSlotDTOs
{
    public class CreateTimeSlotDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Available { get;} = true;

        public static TimeSlot ToDomain(CreateTimeSlotDTO timeSlotDTO, Guid doctorId)
        {
            return new TimeSlot
            {
                Start = timeSlotDTO.Start,
                End = timeSlotDTO.End,
                Available = timeSlotDTO.Available,
                UserId = doctorId
            };
        }
    }
}
