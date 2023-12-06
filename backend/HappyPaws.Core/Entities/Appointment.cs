using HappyPaws.Core.Entities.Common;
using HappyPaws.Core.Enums;

namespace HappyPaws.Core.Entities
{
    public class Appointment : Entity, IUserOwnedResource
    {
        public AppointmentStatus Status { get; set; }
        public Guid PetId { get; set; }
        public Guid TimeSlotId { get; set; }
        public Pet Pet { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public List<Note>? Notes { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<AppointmentProcedure> AppointmentProcedures { get; set; } = new List<AppointmentProcedure> (){ };

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            if(AppointmentProcedures.Count != 0)
            {
                foreach (var procedure in AppointmentProcedures)
                {
                    totalPrice += procedure.Procedure.Price;
                }
            }

            return totalPrice;
        }
    }
}
