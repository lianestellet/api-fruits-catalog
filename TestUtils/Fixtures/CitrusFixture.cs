using Entities.Domain;

namespace TestUtils.Fixtures
{
    public static class CitrusFixture
    {
        public static readonly FruitType Type = 
            new("Citrus")
            { 
                Id = 2, 
                Description = "Sour and juicy fruits" 
            };

        public static readonly Fruit Orange = 
            new("Orange") 
            { 
                Description = "Juicy and tangy", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Lemon = 
            new("Lemon") 
            { 
                Description = "Sour and yellow", 
                FruitTypeId = Type.Id 
            };

        public static readonly List<Fruit> AllCitrus = [Orange, Lemon];
    }
}
