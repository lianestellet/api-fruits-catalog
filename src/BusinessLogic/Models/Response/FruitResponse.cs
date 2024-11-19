using BusinessLogic.Models.DTOs;

namespace BusinessLogic.Models.Response
{
    public class FruitResponse (FruitDTO fruit, string message = "") : BaseResponse(message)
    {
        public long Id { get; set; } = fruit.Id;
        public string Name { get; set; } = fruit.Name;
        public string Description { get; set; } = fruit.Description;
        public long FruitTypeId { get; set; } = fruit.FruitTypeId;
        public string FruitTypeName { get; set; } = fruit.FruitTypeName;
        public string FruitTypeDescription { get; set; } = fruit.FruitTypeDescription;

    }
}
