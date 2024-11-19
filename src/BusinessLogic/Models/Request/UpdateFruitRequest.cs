namespace BusinessLogic.Models.Request
{
    public class UpdateFruitRequest
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long FruitTypeId { get; set; }
    }
}
