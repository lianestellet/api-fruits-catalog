using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Fixtures;

namespace DataAccess.Context
{
    public static class InitializeDbContext
    {
        public static void Seed(FruitDbContext context)
        {
            if ( context.Fruits.Any() || context.FruitTypes.Any())
            {
                return;
            }

            List<FruitType> fruitTypes =
            [
                BerryFixture.Type,
                TropicalFixture.Type,
                CitrusFixture.Type,
                StoneFixture.Type,
                PomeFixture.Type
            ];

            context.FruitTypes.AddRange(fruitTypes);
            context.SaveChanges();

            List<Fruit> fruits =
            [
                BerryFixture.Strawberry,
                TropicalFixture.Mango,
                CitrusFixture.Orange,
                StoneFixture.Cherry,
                PomeFixture.Apple
            ];

            context.Fruits.AddRange(fruits);
            context.SaveChanges();

        }
    }
}
