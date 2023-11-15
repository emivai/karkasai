using FluentValidation;
using HappyPaws.API.Contracts.DTOs.UserDTOs;

namespace HappyPaws.API.Validators
{
    public class CreateUserValidator : CustomAbstractValidator<CreateUserDTO>
    {
        private const string OnlyLettersAndWhiteSpace = @"^[\p{L}\s']*$";

        public CreateUserValidator()
        {
            RuleFor(customer => customer.Type).NotNull().WithMessage("Type is required");
            RuleFor(customer => customer.Type).IsInEnum().WithMessage("Type invalid. Valid user type values are: 0 (admin), 1 (client) and 2 (doctor).");

            RuleFor(customer => customer.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(customer => customer.Name).Length(1, 50).WithMessage("Name has to be 1-50 characters long.");
            RuleFor(customer => customer.Name).Matches(OnlyLettersAndWhiteSpace).WithMessage("Name cannot contain numbers or special symbols.");

            RuleFor(customer => customer.Surname).NotEmpty().WithMessage("Surname is required.");
            RuleFor(customer => customer.Surname).Length(1, 50).WithMessage("Surname has to be 1-50 characters long.");
            RuleFor(customer => customer.Surname).Matches(OnlyLettersAndWhiteSpace).WithMessage("Surname cannot contain numbers or special symbols.");

            RuleFor(customer => customer.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(customer => customer.Email).Length(1, 50).WithMessage("Email has to be 1-50 characters long.");

            RuleFor(customer => customer.PhoneNumber).NotEmpty().WithMessage("Phone number is required and must be 1-15 characters long.");
            RuleFor(customer => customer.PhoneNumber).Length(1, 15).WithMessage("Phone number has to be 1-15 characters long.");
        }
    }

    public class UpdateUserValidator : CustomAbstractValidator<UpdateUserDTO>
    {
        private const string OnlyLettersAndWhiteSpace = @"^[\p{L}\s']*$";

        public UpdateUserValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(customer => customer.Name).Length(1, 50).WithMessage("Name has to be 1-50 characters long.");
            RuleFor(customer => customer.Name).Matches(OnlyLettersAndWhiteSpace).WithMessage("Name cannot contain numbers or special symbols.");

            RuleFor(customer => customer.Surname).NotEmpty().WithMessage("Surname is required.");
            RuleFor(customer => customer.Surname).Length(1, 50).WithMessage("Surname has to be 1-50 characters long.");
            RuleFor(customer => customer.Surname).Matches(OnlyLettersAndWhiteSpace).WithMessage("Surname cannot contain numbers or special symbols.");

            RuleFor(customer => customer.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(customer => customer.Email).Length(1, 50).WithMessage("Email has to be 1-50 characters long.");

            RuleFor(customer => customer.PhoneNumber).NotEmpty().WithMessage("Phone number is required and must be 1-15 characters long.");
            RuleFor(customer => customer.PhoneNumber).Length(1, 15).WithMessage("Phone number has to be 1-15 characters long.");
        }
    }
}
