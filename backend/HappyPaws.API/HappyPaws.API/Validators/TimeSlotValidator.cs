using FluentValidation;
using HappyPaws.API.Contracts.DTOs.TimeSlotDTOs;

namespace HappyPaws.API.Validators
{
    public class CreateTimeSlotValidator : CustomAbstractValidator<CreateTimeSlotDTO>
    {
        public CreateTimeSlotValidator() 
        { 
            RuleFor(timeSlot => timeSlot.Start).NotEmpty().WithMessage("Start is required.");
            RuleFor(timeSlot => timeSlot.Start).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Start has to be in the future.");
            RuleFor(timeSlot => timeSlot.Start).LessThan(DateTime.UtcNow.AddYears(1)).WithMessage("Start cannot be more than 1 year in the future.");

            RuleFor(timeSlot => timeSlot.End).NotEmpty().WithMessage("End is required.");
            RuleFor(timeSlot => timeSlot.End).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("End has to be in the future.");
            RuleFor(timeSlot => timeSlot.End).GreaterThanOrEqualTo(timeSlot => timeSlot.Start.AddMinutes(15)).WithMessage("End has to be at least 15 minutes later than start.");
            RuleFor(timeSlot => timeSlot.End).LessThan(DateTime.UtcNow.AddYears(1)).WithMessage("End cannot be more than 1 year in the future.");

            RuleFor(timeSlot => timeSlot.DoctorId).NotEmpty().WithMessage("DoctorId is required.");
        }
    }

    public class UpdateTimeSlotValidator : CustomAbstractValidator<UpdateTimeSlotDTO>
    {
        public UpdateTimeSlotValidator()
        {
            RuleFor(timeSlot => timeSlot.Start).NotEmpty().WithMessage("Start is required.");
            RuleFor(timeSlot => timeSlot.Start).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Start has to be in the future.");
            RuleFor(timeSlot => timeSlot.Start).LessThan(DateTime.UtcNow.AddYears(1)).WithMessage("Start cannot be more than 1 year in the future.");

            RuleFor(timeSlot => timeSlot.End).NotEmpty().WithMessage("End is required.");
            RuleFor(timeSlot => timeSlot.End).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("End has to be in the future.");
            RuleFor(timeSlot => timeSlot.End).GreaterThanOrEqualTo(timeSlot => timeSlot.Start.AddMinutes(15)).WithMessage("End has to be at least 15 minutes later than start.");
            RuleFor(timeSlot => timeSlot.End).LessThan(DateTime.UtcNow.AddYears(1)).WithMessage("End cannot be more than 1 year in the future.");

            RuleFor(timeSlot => timeSlot.DoctorId).NotEmpty().WithMessage("DoctorId is required.");
        }
    }
}
