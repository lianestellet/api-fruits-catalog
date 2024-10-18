using DataAccess.Context;
using Entities.Domain;

namespace DataAccess.Repositories
{
    public class MockFruitRepository : FruitRepository
    {
        private readonly List<Fruit> _fruits;

        public MockFruitRepository(FruitDbContext context) : base(context)
        {
            _fruits = new List<Fruit> {

            new Fruit(1, "Banana", "Peel before eating"),
            new Fruit(2, "Apple", "Peel before eating"),
            new Fruit(3, "Papaya", "Remove the peel before eating"),
            new Fruit(4, "Melon", "Remove the peel before eating")  }
            ;
        }

        public IEnumerable<Fruit> FindAllFruits() => _fruits.ToList();

        public Fruit FindFruitById(long id) => _fruits.First(fruit => fruit.Id == id);

        public void SaveFruit(Fruit fruit) => _fruits.Add(fruit);

        public void UpdateFruit(Fruit updatedFruit)
        {
            Fruit? updatingFruit = _fruits.FirstOrDefault(fruit => fruit.Id == updatedFruit.Id);
            if (updatingFruit != null) {
                updatedFruit.Description = updatedFruit.Description;
                updatedFruit.Name = updatedFruit.Name;
            }
        }

        public void DeleteFruit(long id)
        {
            _fruits.RemoveAll(fruit => fruit.Id == id);
        }
    }
}
