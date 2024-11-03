using Entities.Domain;

namespace TestUtils.Data
{
    public interface ISeedData
    {
        public List<FruitType> SetFruitTypes();
        public List<Fruit> SetFruits();
    }
}
