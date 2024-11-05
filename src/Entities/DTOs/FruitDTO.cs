using Entities.Domain;

namespace Entities.DTOs
{
    public class FruitDTO : BaseFruitDTO
    {
        public long Id { get; set; }
        public FruitTypeDTO? FruitType { get; set; }

        public FruitDTO (Fruit fruit)
        {
            Id = fruit.Id;
            Name = fruit.Name;
            Description = fruit.Description;
            FruitTypeId = fruit.FruitTypeId;
        }

        public FruitDTO()
        {
            
        }
    }
}
