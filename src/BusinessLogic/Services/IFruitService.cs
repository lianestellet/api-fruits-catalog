using API.Models.Request;
using BusinessLogic.Models.DTOs;
using BusinessLogic.Models.Request;
using BusinessLogic.Models.Response;

namespace BusinessLogic.Services
{
    public interface IFruitService
    {
        Task<FruitListResponse> FindAllFruitsAsync();
        Task<FruitResponse> FindFruitByIdAsync(long id);
        Task<FruitResponse> SaveFruitAsync(SaveFruitRequest fruit);
        Task<FruitResponse> UpdateFruitAsync(UpdateFruitRequest fruitDto);
        Task DeleteFruitAsync(long id);
    }
}
