using Entities.Domain;

namespace Utils.Data
{
    public interface ISeedData
    {
        public List<FruitType> SetFruitTypes();
        public List<Fruit> SetFruits();
    }
}
