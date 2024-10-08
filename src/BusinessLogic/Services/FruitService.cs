using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal class FruitService : IFruitService
    {
        private readonly IFruitRepository _fruitRepository;

        public FruitService(IFruitRepository fruitRepository) => _fruitRepository = fruitRepository;

        public IEnumerable<Fruit> FindAllFruits() => _fruitRepository.FindAllFruits();

        public Fruit FindFruitById(long id) => _fruitRepository.FindFruitById(id);

        public void SaveFruit(Fruit fruit) => throw new NotImplementedException();

        public void UpdateFruit(long id, Fruit updatedFruit) => throw new NotImplementedException();

        public void DeleteFruit(long id) => _fruitRepository.DeleteFruit(id);
    }
}
