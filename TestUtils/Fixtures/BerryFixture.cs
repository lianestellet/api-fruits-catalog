using Entities.Domain;

namespace TestUtils.Fixtures
{
    public static class BerryFixture
    {
        public static readonly FruitType Type = 
            new("Berry") 
            { 
                Id = 1, 
                Description = "Small, pulpy, and often edible fruit" 
            };

        public static readonly Fruit Strawberry =
            new("Strawberry") 
            { 
                Description = "Sweet and red", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Blueberry =
            new("Blueberry") 
            { 
                Description = "Small and blue", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Raspberry =
            new("Raspberry") 
            { 
                Description = "Delicate and tart", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Blackberry =
            new("Blackberry") 
            { 
                Description = "Sweet and dark", 
                FruitTypeId = Type.Id 
            };

        public static readonly List<Fruit> AllBerries = [Strawberry, Blueberry, Raspberry, Blackberry];

    }
}
