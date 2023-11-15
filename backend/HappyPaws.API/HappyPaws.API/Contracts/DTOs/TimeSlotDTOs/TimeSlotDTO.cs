using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.TimeSlotDTOs
{
    public class TimeSlotDTO
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Available { get; set; }
        public Guid DoctorId { get; set; }

        public static TimeSlotDTO FromDomain(TimeSlot timeSlot)
        {
            return new TimeSlotDTO
            {
                Id = timeSlot.Id,
                Start = timeSlot.Start,
                End = timeSlot.End,
                Available = timeSlot.Available,
                DoctorId = timeSlot.DoctorId
            };
        }
    }
}
