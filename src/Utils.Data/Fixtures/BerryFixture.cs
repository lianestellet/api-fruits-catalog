using Entities.Domain;

namespace Utils.Fixtures
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
                Id = 1,
                Description = "Sweet and red", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Blueberry =
            new("Blueberry")
            {
                Id = 2,
                Description = "Small and blue", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Raspberry =
            new("Raspberry") 
            {

                Id = 3,
                Description = "Delicate and tart", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Blackberry =
            new("Blackberry") 
            {
                Id = 4,
                Description = "Sweet and dark", 
                FruitTypeId = Type.Id 
            };

        public static readonly List<Fruit> AllBerries = [Strawberry, Blueberry, Raspberry, Blackberry];

    }
}
