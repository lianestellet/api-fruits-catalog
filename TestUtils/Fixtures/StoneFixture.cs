using Entities.Domain;

namespace TestUtils.Fixtures
{
    public static class StoneFixture
    {
        public static readonly FruitType Type = 
            new("Stone Fruit")
            { 
                Id = 4, 
                Description = "Fruits with a large, hard pit" 
            };

        public static readonly Fruit Peach = 
            new("Peach")
            { 
                Description = "Soft and fuzzy", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Cherry = 
            new("Cherry")
            { 
                Description = "Small and juicy", 
                FruitTypeId = Type.Id 
            };

        public static readonly List<Fruit> AllStoneFruits = [Peach, Cherry];
    }
}
