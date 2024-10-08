using DataAccess.Models;

namespace DataAccess.Repository
{
    internal class MockFruitRepository : IFruitRepository
    {
        private readonly List<Fruit> _fruits = new List<Fruit> {
            new (1, "Banana", "Peel before eating"),
            new (2, "Apple", "Peel before eating"),
            new (3, "Papaya"),
            new (4, "Melon")
        };

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
