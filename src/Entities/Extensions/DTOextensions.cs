using Entities.Domain;
using Entities.DTOs;

namespace Entities.Extensions
{
    public static class DTOextensions
    {
        public static Fruit ToFruit(this FruitDTO? fruit)
        {
            return fruit == null 
                ? new Fruit()
                : new Fruit
                {
                    Id = fruit.Id,
                    Name = fruit.Name,
                    Description = fruit.Description,
                    FruitTypeId = fruit.FruitTypeId,
                };
        }

        public static Fruit ToFruit(this CreateFruitDTO fruit)
        {
            return new Fruit
                {
                    Name = fruit.Name,
                    Description = fruit.Description,
                    FruitTypeId = fruit.FruitTypeId,
                };
        }
    }
}
