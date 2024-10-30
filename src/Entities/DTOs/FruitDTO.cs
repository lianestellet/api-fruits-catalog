using Entities.Domain;

namespace Entities.DTOs
{
    public class FruitDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? FruitTypeId { get; set; }
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
