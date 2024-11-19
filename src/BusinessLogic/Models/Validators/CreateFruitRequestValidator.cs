using API.Models.Request;
using FluentValidation;

namespace API.Models.Validators
{
    public class CreateFruitRequestValidator : AbstractValidator<SaveFruitRequest>
    {
        public CreateFruitRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorMessages.FruitNameRequired);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(ValidatorMessages.FruitDescriptionRequired)
                .When(x => !string.IsNullOrEmpty(x.Description))
                .MinimumLength(12).WithMessage(ValidatorMessages.FruitDescriptionMinLength);

            RuleFor(x => x.FruitTypeId)
                .GreaterThan(0).WithMessage(ValidatorMessages.FruitTypeIdRequired);
        }
    }
}