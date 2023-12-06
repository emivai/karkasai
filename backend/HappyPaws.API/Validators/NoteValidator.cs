using FluentValidation;
using HappyPaws.API.Contracts.DTOs.NoteDTOs;

namespace HappyPaws.API.Validators
{
    public class CreateNoteValidator : CustomAbstractValidator<CreateNoteDTO>
    {
        public CreateNoteValidator() 
        {
            RuleFor(note => note.Value).NotEmpty().WithMessage("Value is required.");
            RuleFor(note => note.Value).Length(1, 250).WithMessage("Value has to be 1-250 characters long.");
        }
    }

    public class UpdateNoteValidator : CustomAbstractValidator<UpdateNoteDTO>
    {
        public UpdateNoteValidator()
        {
            RuleFor(note => note.Value).NotEmpty().WithMessage("Value is required.");
            RuleFor(note => note.Value).Length(1, 250).WithMessage("Value has to be 1-250 characters long.");
        }
    }
}
