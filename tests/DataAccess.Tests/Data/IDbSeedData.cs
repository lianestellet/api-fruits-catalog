using Entities.Domain;

namespace DataAccess.Tests.Data
{

    public interface IDbSeedData
    {
        public List<FruitType> SetFruitTypes();
        public List<Fruit> SetFruits();
    }
}
