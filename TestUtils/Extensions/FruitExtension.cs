using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtils.Extensions
{
    public static class FruitExtension
    {
        public static Fruit WithId(this Fruit fruit, long id)
        {
            fruit.Id = id;
            return fruit;
        }

        public static void UpdateDetails(this Fruit fruit, Fruit updatedFruit)
        {
            fruit.Name = updatedFruit.Name;
            fruit.Description = updatedFruit.Description;
            fruit.FruitTypeId = updatedFruit.FruitTypeId;
        }
    }
}
