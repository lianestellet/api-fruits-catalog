using Entities.Domain;
using Utils.Fixtures;

namespace Utils.Data
{
    public class FruitSeedData : ISeedData
    {
        public FruitSeedData(Fruit? fruit = null)
        {
            if(fruit != null)
                Fruits.Add(fruit);
        }

        public List<Fruit> Fruits = 
            [
                BerryFixture.Strawberry, 
                TropicalFixture.Mango, 
                CitrusFixture.Orange, 
                StoneFixture.Cherry, 
                PomeFixture.Apple
            ];

        public List<Fruit> SetFruits() => Fruits;

        public List<FruitType> SetFruitTypes() =>
            [
                BerryFixture.Type,
                TropicalFixture.Type,
                CitrusFixture.Type,
                StoneFixture.Type,
                PomeFixture.Type
            ];
    }
}
