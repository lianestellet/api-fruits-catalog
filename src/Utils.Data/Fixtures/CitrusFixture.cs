using Entities.Domain;

namespace Utils.Fixtures
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
                Id = 11,
                Description = "Juicy and tangy", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Lemon = 
            new("Lemon") 
            {
                Id = 12,
                Description = "Sour and yellow", 
                FruitTypeId = Type.Id 
            };

        public static readonly List<Fruit> AllCitrus = [Orange, Lemon];
    }
}
