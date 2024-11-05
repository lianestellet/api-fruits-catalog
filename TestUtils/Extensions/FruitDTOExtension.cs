using Entities.Domain;
using Entities.DTOs;

namespace TestUtils.Extensions
{
    public static class FruitDTOExtension
    {
        public static Fruit ToFruit(this FruitDTO fruitDto)
        {
            return new Fruit
            {
                Name = fruitDto.Name,
                Description = fruitDto.Description,
                FruitTypeId = fruitDto.FruitTypeId,
                Id = fruitDto.Id
            };
        }

        public static Fruit ToFruit(this CreateFruitDTO fruitDto)
        {
            return new Fruit
            {
                Name = fruitDto.Name,
                Description = fruitDto.Description,
                FruitTypeId = fruitDto.FruitTypeId
            }; ;
        }
    }
}
