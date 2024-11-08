using Entities.Domain;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.Extensions;
using Entities.Interfaces;
using FluentValidation;

namespace BusinessLogic.Services
{
    public class FruitService(IFruitRepository fruitRepository, IValidator<FruitDTO> validator, IValidator<CreateFruitDTO> createValidator) : IFruitService
    {
        private readonly IFruitRepository _fruitRepository = fruitRepository;
        private readonly IValidator<FruitDTO> _validator = validator;
        private readonly IValidator<CreateFruitDTO> _createValidator = createValidator;

        public async Task<IEnumerable<FruitDTO>> FindAllFruitsAsync()
        {
            var fruits = await _fruitRepository.FindAllFruitsAsync();
            return fruits.Select(fruit => new FruitDTO(fruit)).ToList();
        }

        public async Task<FruitDTO> FindFruitByIdAsync(long id)
        {
            var fruit = await _fruitRepository.FindFruitByIdAsync(id);

            return fruit == null 
                ? throw new NotFoundException(ExceptionMessages.FruitNotFoundById(id)) 
                : new FruitDTO(fruit);
        }

        public async Task<FruitDTO> SaveFruitAsync(CreateFruitDTO fruit)
        {
            var validationResult = _createValidator.Validate(fruit);
            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await FindFruitTypeById(fruit.FruitTypeId);

            var savedFruit = await _fruitRepository.SaveFruitAsync(fruit.ToFruit());
            return savedFruit.ToFruitDTO();
        }

        public async Task<FruitDTO> UpdateFruitAsync(FruitDTO fruitDto) 
        {
            var validationResult = _validator.Validate(fruitDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await FindFruitTypeById(fruitDto.FruitTypeId);
            var result = await _fruitRepository.UpdateFruitAsync(fruitDto.ToFruit());
            return result.ToFruitDTO();
        }

        public async Task DeleteFruitAsync(long id)
        {
            await FindFruitByIdAsync(id);
            await _fruitRepository.DeleteFruitAsync(id);
        }

        private async Task<FruitType?> FindFruitTypeById(long fruitId)
        {
            var fruitType = await _fruitRepository.FruitTypeByIdAsync(fruitId);
            return fruitType ?? throw new NotFoundException(ExceptionMessages.FruitTypeNotFoundById(fruitId));
        }
    }
}
