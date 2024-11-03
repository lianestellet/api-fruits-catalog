using Entities.Domain;

namespace TestUtils.Fixtures
{
    public static class PomeFixture
    {
        public static readonly FruitType Type = 
            new("Pome") 
            { 
                Id = 3, 
                Description = "Fruits that are typically fleshy with seeds in a core" 
            };

        public static readonly Fruit Apple = 
            new("Apple") 
            { 
                Description = "Sweet and crunchy", 
                FruitTypeId = Type.Id 
            };

        public static readonly Fruit Pear = new("Pear") 
        { 
            Description = "Juicy and soft", 
            FruitTypeId = Type.Id 
        };

        public static readonly List<Fruit> AllPomes = [Apple, Pear];
    }
}
