using FluentValidation;
using HappyPaws.API.Contracts.DTOs.AppointmentProcedureDTOs;

namespace HappyPaws.API.Validators
{
    public class CreateAppointmentProcedureValidator : CustomAbstractValidator<CreateAppointmentProcedureDTO>
    {
        public CreateAppointmentProcedureValidator()
        {
            RuleFor(appointmentProcedure => appointmentProcedure.ProcedureId).NotEmpty().WithMessage("ProcedureId is required.");
        }
    }

    public class UpdateAppointmentProcedureValidator : CustomAbstractValidator<UpdateAppointmentProcedureDTO>
    {
        public UpdateAppointmentProcedureValidator()
        {
            RuleFor(appointmentProcedure => appointmentProcedure.ProcedureId).NotEmpty().WithMessage("ProcedureId is required.");

            RuleFor(appointmentProcedure => appointmentProcedure.AppointmentId).NotEmpty().WithMessage("AppointmentId is required.");
        }
    }
}
