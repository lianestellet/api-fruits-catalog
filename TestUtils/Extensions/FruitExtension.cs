using Entities.Domain;
using Entities.DTOs;

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
