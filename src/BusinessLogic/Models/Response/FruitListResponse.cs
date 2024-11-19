using BusinessLogic.Models.DTOs;

namespace BusinessLogic.Models.Response
{
    public class FruitListResponse(List<FruitDTO> fruitList, string message = "") : BaseResponse(message)
    {
        public List<FruitDTO> Fruits { get; } = fruitList;
    }
}
