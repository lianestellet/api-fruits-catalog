namespace BusinessLogic.Models.DTOs
{
    public class FruitDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long FruitTypeId { get; set; }
        public string FruitTypeName { get; set; } = string.Empty;
        public string FruitTypeDescription { get; set; } = string.Empty;
    }
}
