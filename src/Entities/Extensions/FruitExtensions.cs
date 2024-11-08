using Entities.Domain;
using Entities.DTOs;

namespace Entities.Extensions
{
    public static class FruitExtensions
    {
        public static FruitDTO ToFruitDTO(this Fruit fruit, long? fruitId = null)
        {
            return new FruitDTO
            {
                Id = fruitId == null ? fruit.Id : fruitId.Value,
                Name = fruit.Name,
                Description = fruit.Description,
                FruitTypeId = fruit.FruitTypeId,
                FruitType = fruit.FruitType?.ToFruitTypeDTO()
            };
        }

        public static CreateFruitDTO ToCreateFruitDTO(this Fruit fruit)
        {
            return new CreateFruitDTO
            {
                Name = fruit.Name,
                Description = fruit.Description,
                FruitTypeId = fruit.FruitTypeId
            };

        }

        public static FruitTypeDTO ToFruitTypeDTO(this FruitType fruitType)
        {
            return new FruitTypeDTO
            {
                Id = fruitType.Id,
                Name = fruitType.Name,
                Description = fruitType.Description
            };
        }

        public static IEnumerable<FruitDTO> ToFruitDTOList(this IEnumerable<Fruit> fruits) 
        {
            return fruits.Select(fruit => fruit.ToFruitDTO()).ToList();
        }
        public static void ApplyAttributesFrom(this Fruit fruit, Fruit updatedFruit)
        {
            fruit.Name = updatedFruit.Name;
            fruit.Description = updatedFruit.Description;
            fruit.FruitTypeId = updatedFruit.FruitTypeId;
        }
    }
}
