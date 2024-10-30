using Entities.DTOs;
using Entities.Interfaces;

namespace BusinessLogic.Services
{
    public class FruitService(IFruitRepository fruitRepository) : IFruitService
    {
        private readonly IFruitRepository _fruitRepository = fruitRepository;

        public async Task<IEnumerable<FruitDTO>> FindAllFruits()
        {
            var fruits = await _fruitRepository.FindAllFruitsAsync();
            return fruits.Select(fruit => new FruitDTO(fruit));
        }

        public Task<FruitDTO> FindFruitById(long id) => throw new NotImplementedException();        

        public Task<FruitDTO> SaveFruit(FruitDTO fruit) => throw new NotImplementedException();

        public Task<FruitDTO> UpdateFruit(FruitDTO fruit) => throw new NotImplementedException();

        public async Task DeleteFruit(long id) => throw new NotImplementedException();
    }
}
