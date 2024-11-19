namespace API.Models.Request
{
    public class SaveFruitRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long FruitTypeId { get; set; } = 0;
    }
}
