using FluentValidation;
using FluentValidation.Results;

namespace HappyPaws.API.Validators
{
    public class CustomAbstractValidator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            return validationResult;
        }
    }
}
