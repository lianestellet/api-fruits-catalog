using Entities.Domain;

namespace TestUtils.Fixtures
{
    public static class TropicalFruitFixture
    {
        public static readonly FruitType Type = new("Tropical")
        {
            Id = 4,
            Description = "Known for their exotic flavors and vibrant colors"
        };

        public static readonly Fruit Pineapple = new("Pineapple")
        {
            Id = 1,
            Description = "Tropical and tangy",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Mango = new("Mango")
        {
            Id = 2,
            Description = "Sweet and juicy",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Papaya = new("Papaya")
        {
            Id = 3,
            Description = "Soft and sweet",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Coconut = new("Coconut")
        {
            Id = 4,
            Description = "Rich and creamy",
            FruitTypeId = Type.Id
        };

        public static readonly List<Fruit> AllTropicalFruits = [Pineapple, Mango, Papaya, Coconut ];
    }
}
