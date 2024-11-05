namespace Entities.DTOs
{
    public class BaseFruitDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long FruitTypeId { get; set; }
    }
}
