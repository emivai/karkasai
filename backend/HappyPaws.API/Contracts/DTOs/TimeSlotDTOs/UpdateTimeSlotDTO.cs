using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.TimeSlotDTOs
{
    public class UpdateTimeSlotDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Available { get; set; }

        public static TimeSlot ToDomain(UpdateTimeSlotDTO timeSlotDTO, Guid doctorId)
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
