using Entities.Domain;

namespace Utils.Fixtures
{
    public static class StoneFixture
    {
        public static readonly FruitType Type = 
            new("Stone FruitMessages")
            { 
                Id = 4, 
                Description = "Fruits with a large, hard pit" 
            };

        public static readonly Fruit Peach = 
            new("Peach")
            { 
                Id = 31,
                Description = "Soft and fuzzy", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Cherry = 
            new("Cherry")
            { 
                Id = 32,
                Description = "Small and juicy", 
                FruitTypeId = Type.Id 
            };

        public static readonly List<Fruit> AllStoneFruits = [Peach, Cherry];
    }
}
