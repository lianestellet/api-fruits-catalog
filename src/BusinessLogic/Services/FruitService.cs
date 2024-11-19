using API.Models.Request;
using AutoMapper;
using BusinessLogic.Models.DTOs;
using BusinessLogic.Models.Request;
using BusinessLogic.Models.Response;
using DataAccess.Repositories;
using Entities.Domain;
using Entities.Exceptions;

namespace BusinessLogic.Services
{
    public class FruitService(IFruitRepository fruitRepository, IMapper mapper) : IFruitService
    {
        private readonly IFruitRepository _fruitRepository = fruitRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<FruitListResponse> FindAllFruitsAsync()
        {
            var fruits = await _fruitRepository.FindAllFruitsAsync();
            var fruitList = _mapper.Map<IEnumerable<FruitDTO>>(fruits).ToList();
            return new FruitListResponse(fruitList);
        }

        public async Task<FruitResponse> FindFruitByIdAsync(long id)
        {
            var fruit = await FindFruitByIdOrThrowException(id);
            var fruitDto = _mapper.Map<FruitDTO>(fruit);
            return new FruitResponse(fruitDto, "Fruit retrieved successfully");
        }

        public async Task<FruitResponse> SaveFruitAsync(SaveFruitRequest createFruitRequest)
        {
            await ValidateFruitTypeAsync(createFruitRequest.FruitTypeId);                        

            var createdFruit = await _fruitRepository.SaveFruitAsync(_mapper.Map<Fruit>(createFruitRequest));

            return new FruitResponse(_mapper.Map<FruitDTO>(createdFruit), "Fruit created successfully");
        }

        public async Task<FruitResponse> UpdateFruitAsync(UpdateFruitRequest updateFruitRequest) 
        {
            await ValidateFruitTypeAsync(updateFruitRequest.FruitTypeId);

            var fruit = await FindFruitByIdOrThrowException(updateFruitRequest.Id);

            fruit.Id = fruit.Id;
            fruit.Name = updateFruitRequest.Name;
            fruit.Description = updateFruitRequest.Description;
            fruit.FruitTypeId = updateFruitRequest.FruitTypeId;
            
            var updatedFruit = await _fruitRepository.UpdateFruitAsync(fruit);

            var fruitDto = _mapper.Map<FruitDTO>(updatedFruit);
            return new FruitResponse(fruitDto, "Fruit Updated successfully");
        }

        private async Task<FruitType> ValidateFruitTypeAsync(long fruitTypeId)
        {
            var fruitType = await _fruitRepository.FindFruitTypeByIdAsync(fruitTypeId);
            return fruitType ?? throw new NotFoundException(ExceptionMessages.FruitTypeNotFoundById(fruitTypeId));
        }

        private async Task<Fruit> FindFruitByIdOrThrowException(long fruitId)
        {
            var fruit = await _fruitRepository.FindFruitByIdAsync(fruitId);
            return fruit ?? throw new NotFoundException(ExceptionMessages.FruitNotFoundById(fruitId));
        }

        public async Task DeleteFruitAsync(long id)
        {
            await FindFruitByIdAsync(id);
            await _fruitRepository.DeleteFruitAsync(id);
        }
    }
}
