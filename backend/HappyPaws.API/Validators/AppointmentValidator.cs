using FluentValidation;
using HappyPaws.API.Contracts.DTOs.AppointmentDTOs;

namespace HappyPaws.API.Validators
{
    public class CreateAppointmentValidator : CustomAbstractValidator<CreateAppointmentDTO>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(pet => pet.TimeSlotId).NotNull().WithMessage("TimeSlotId is required.");
        }
    }

    public class UpdateAppointmentValidator : CustomAbstractValidator<UpdateAppointmentDTO>
    {
        public UpdateAppointmentValidator() 
        {
            RuleFor(pet => pet.Status).NotNull().WithMessage("Status is required.");
            RuleFor(pet => pet.Status).IsInEnum().WithMessage("Status invalid. Valid status values are: 0 (scheduled), 1 (cancelled) and 2 (done).");

            RuleFor(pet => pet.TimeSlotId).NotNull().WithMessage("TimeSlotId is required.");
        }
    }
}
