using Core.DTOs;
using Core.Interfaces;

namespace BusinessLogic.Services
{
    internal class FruitService : IFruitService
    {
        private readonly IFruitRepository _fruitRepository;

        public FruitService(IFruitRepository fruitRepository) => _fruitRepository = fruitRepository;

        public async Task<IEnumerable<FruitDTO>> FindAllFruits() => throw new NotImplementedException();
 
        public Task<FruitDTO> FindFruitById(long id) => throw new NotImplementedException();        

        public Task SaveFruit(FruitDTO fruit) => throw new NotImplementedException();

        public Task UpdateFruit(FruitDTO fruit) => throw new NotImplementedException();

        public async Task DeleteFruit(long id) => throw new NotImplementedException();
    }
}
