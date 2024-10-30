using Entities.DTOs;
using FluentValidation;

namespace Entities.Validation
{
    public class FruitDTOValidator : AbstractValidator<FruitDTO>
    {
        public FruitDTOValidator() 
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("");
            RuleFor(f => f.Description).NotEmpty().WithMessage("Fruit description is required.");            
        }
    }
}
