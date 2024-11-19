using BusinessLogic.Models.Request;
using FluentValidation;

namespace API.Models.Validators
{
    public class UpdateFruitRequestValidator : AbstractValidator<UpdateFruitRequest>
    {
        public UpdateFruitRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ValidatorMessages.FruitIdRequired);

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
