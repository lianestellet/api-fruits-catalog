using Entities.Domain;
using Utils.Fixtures;

namespace Utils.Data
{
    public class SeedData() : ISeedData
    {
        public List<Fruit> Fruits { get; set; } = [];

        public List<FruitType> FruitTypes { get; set; } = [];

        public List<Fruit> SetFruits() => Fruits;

        public List<FruitType> SetFruitTypes() => FruitTypes;

        public SeedData SeedFruit(Fruit fruit)
        {
            Fruits.Add(fruit);
            return this;
        }

        public SeedData SeedFruits(List<Fruit> fruits)
        {
            Fruits.AddRange(fruits);
            return this;
        }

        public SeedData SeedFruitType(FruitType fruitType)
        {
            FruitTypes.Add(fruitType);
            return this;
        }

        public SeedData SeedFruitTypes(List<FruitType> fruitTypes)
        {
            FruitTypes.AddRange(fruitTypes);
            return this;
        }
    
        public SeedData SeedAllFruitTypes()
        {
            List<FruitType> allTypes = 
                [
                    BerryFixture.Type, 
                    CitrusFixture.Type, 
                    PomeFixture.Type, 
                    StoneFixture.Type, 
                    TropicalFixture.Type
                ];
            FruitTypes.AddRange(allTypes);
            return this;
        }

        public SeedData SeedAllFruits()
        {
            Fruits.AddRange(BerryFixture.AllBerries);
            Fruits.AddRange(CitrusFixture.AllCitrus);
            Fruits.AddRange(PomeFixture.AllPomes);
            Fruits.AddRange(StoneFixture.AllStoneFruits);
            Fruits.AddRange(TropicalFixture.AllTropicalFruits);
            return this;
        }

        public ISeedData SeedBerriesFruits()
        {
            FruitTypes.Add(BerryFixture.Type);
            Fruits.AddRange(BerryFixture.AllBerries);
            return this;
        }
    }
}
