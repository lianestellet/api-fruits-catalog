using Entities.Domain;

namespace DataAccess.Tests.Data
{
    public class DbSeedData : IDbSeedData
    {
        public List<FruitType> SetFruitTypes() => [
                new FruitType("Pome") 
                {
                    Id = 1,
                    Description = "A generic fruit type",
                },
                new FruitType("Berry") 
                {
                    Id = 2,
                    Description = "A generic fruit type"                    
                },
            ];

        public List<Fruit> SetFruits() => [
                new ("Apple") {
                    Id = 1,
                    Description = "Sweet and crunchy",  
                    FruitTypeId = 1
                },
                new ("Banana") { 
                    Id = 2,
                    Description = "Soft and sweet", 
                    FruitTypeId = 2
                }
            ];
    }
}
