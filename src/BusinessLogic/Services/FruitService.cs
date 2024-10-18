using Entities.DTOs;
using Entities.Interfaces;

namespace BusinessLogic.Services
{
    public class FruitService : IFruitService
    {
        private readonly IFruitRepository _fruitRepository;

        public FruitService(IFruitRepository fruitRepository)
        {
            _fruitRepository = fruitRepository;
        }

        public async Task<IEnumerable<FruitDTO>> FindAllFruits() => await _fruitRepository.FindAllFruits();
 
        public Task<FruitDTO> FindFruitById(long id) => throw new NotImplementedException();        

        public Task<FruitDTO> SaveFruit(FruitDTO fruit) => throw new NotImplementedException();

        public Task<FruitDTO> UpdateFruit(FruitDTO fruit) => throw new NotImplementedException();

        public async Task DeleteFruit(long id) => throw new NotImplementedException();
    }
}
