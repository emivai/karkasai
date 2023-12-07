using FluentValidation;
using HappyPaws.API.Contracts.DTOs.ProcedureDTOs;

namespace HappyPaws.API.Validators
{
    public class CreateProcedureValidator : CustomAbstractValidator<CreateProcedureDTO>
    {
        public CreateProcedureValidator()
        {
            RuleFor(procedure => procedure.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(procedure => procedure.Name).Length(1, 100).WithMessage("Name has to be 1-100 characters long.");

            RuleFor(procedure => procedure.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(procedure => procedure.Description).Length(1, 500).WithMessage("Description has to be 1-250 characters long.");

            RuleFor(procedure => procedure.Price).NotEmpty().WithMessage("Price is required.");
            RuleFor(procedure => procedure.Price).InclusiveBetween(0, 10000).WithMessage("Price has to be in range 0 - 10000.");
        }      
    }

    public class UpdateProcedureValidator : CustomAbstractValidator<UpdateProcedureDTO>
    {
        public UpdateProcedureValidator()
        {
            RuleFor(procedure => procedure.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(procedure => procedure.Name).Length(1, 100).WithMessage("Name has to be 1-100 characters long.");

            RuleFor(procedure => procedure.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(procedure => procedure.Description).Length(1, 500).WithMessage("Description has to be 1-250 characters long.");
            
            RuleFor(procedure => procedure.Price).NotNull().WithMessage("Price is required.");
            RuleFor(procedure => procedure.Price).InclusiveBetween(0, 10000).WithMessage("Price has to be in range 0 - 10000.");
        }
    }
}
