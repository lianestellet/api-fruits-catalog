using Entities.Domain;

namespace TestUtils.Fixtures
{
    public static class TropicalFixture
    {
        public static readonly FruitType Type = new("Tropical")
        {
            Id = 5,
            Description = "Known for their exotic flavors and vibrant colors"
        };

        public static readonly Fruit Pineapple = new("Pineapple")
        {
            Description = "Tropical and tangy",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Mango = new("Mango")
        {
            Description = "Sweet and juicy",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Papaya = new("Papaya")
        {
            Description = "Soft and sweet",
            FruitTypeId = Type.Id
        };

        public static readonly Fruit Coconut = new("Coconut")
        {
            Description = "Rich and creamy",
            FruitTypeId = Type.Id
        };

        public static readonly List<Fruit> AllTropicalFruits = [Pineapple, Mango, Papaya, Coconut ];
    }
}
