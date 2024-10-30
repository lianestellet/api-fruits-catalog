using Entities.Domain;

namespace DataAccess.Tests.Data
{
    internal class CustomDbSeedData(List<FruitType> fruitTypes, List<Fruit>? fruits = null) : IDbSeedData
    {
        public List<Fruit> Fruits { get; set; } = fruits ?? [];

        public List<FruitType> FruitTypes { get; set; } = fruitTypes;

        public List<Fruit> SetFruits() => Fruits;

        public List<FruitType> SetFruitTypes() => FruitTypes;
    }
}
