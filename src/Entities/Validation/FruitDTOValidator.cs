using Entities.DTOs;
using FluentValidation;

namespace Entities.Validation
{
    public class FruitDTOValidator : BaseFruitDTOValidator<FruitDTO>
    {
        public FruitDTOValidator() 
        {
            RuleFor(f => f.Id).NotEmpty().WithMessage(ValidationMessages.FruitIdRequired);
        }
    }
}
