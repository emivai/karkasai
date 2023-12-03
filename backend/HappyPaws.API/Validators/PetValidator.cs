using FluentValidation;
using HappyPaws.API.Contracts.DTOs.PetDTOs;

namespace HappyPaws.API.Validators
{
    public class CreatePetValidator : CustomAbstractValidator<CreatePetDTO>
    {
        private const string OnlyLettersAndWhiteSpace = @"^[\p{L}\s']*$";

        public CreatePetValidator() 
        {
            RuleFor(pet => pet.Type).NotNull().WithMessage("Type is required.");
            RuleFor(pet => pet.Type).IsInEnum().WithMessage("Type invalid. Valid pet type values are: 0 (dog), 1 (cat), 2 (rodent) and 3 (exotic).");

            RuleFor(pet => pet.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(pet => pet.Name).Length(1, 50).WithMessage("Name has to be 1-50 characters long.");
            RuleFor(pet => pet.Name).Matches(OnlyLettersAndWhiteSpace).WithMessage("Name cannot contain numbers or special symbols.");

            RuleFor(pet => pet.Birthdate).NotNull().WithMessage("Birthdate is required.");
            RuleFor(pet => pet.Birthdate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Birthdate cannot be in the future.");
            RuleFor(pet => pet.Birthdate).GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-200)).WithMessage("Birthdate cannot be more than 200 years old.");
        }
    }

    public class UpdatePetValidator : CustomAbstractValidator<UpdatePetDTO>
    {
        private const string OnlyLettersAndWhiteSpace = @"^[\p{L}\s']*$";

        public UpdatePetValidator()
        {
            RuleFor(pet => pet.Type).NotNull().WithMessage("Type is required.");
            RuleFor(pet => pet.Type).IsInEnum().WithMessage("Type invalid. Valid pet type values are: 0 (dog), 1 (cat), 2 (rodent) and 3 (exotic).");

            RuleFor(pet => pet.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(pet => pet.Name).Length(1, 50).WithMessage("Name has to be 1-50 characters long.");
            RuleFor(pet => pet.Name).Matches(OnlyLettersAndWhiteSpace).WithMessage("Name cannot contain numbers or special symbols.");

            RuleFor(pet => pet.Birthdate).NotNull().LessThanOrEqualTo(DateTime.UtcNow);
            RuleFor(pet => pet.Birthdate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Birthdate cannot be in the future.");
            RuleFor(pet => pet.Birthdate).GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-200)).WithMessage("Birthdate cannot be more than 200 years old.");
        }
    }
}
