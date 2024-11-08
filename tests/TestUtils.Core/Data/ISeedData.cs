using Entities.Domain;

namespace TestUtils.Core.Data
{
    public interface ISeedData
    {
        public List<FruitType> SetFruitTypes();
        public List<Fruit> SetFruits();
    }
}
